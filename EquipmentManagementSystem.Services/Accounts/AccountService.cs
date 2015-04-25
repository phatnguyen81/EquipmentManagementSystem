using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using EquipmentManagementSystem.Core;
using EquipmentManagementSystem.Core.Domain.Accounts;
using EquipmentManagementSystem.Core.Caching;
using EquipmentManagementSystem.Core.Data;
using EquipmentManagementSystem.Core.Domain.Catalog;
using EquipmentManagementSystem.Core.Domain.Common;
using EquipmentManagementSystem.Data;

namespace EquipmentManagementSystem.Services.Accounts
{
    /// <summary>
    /// Account service
    /// </summary>
    public partial class AccountService : IAccountService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string ACCOUNTROLES_ALL_KEY = "Ems.accountrole.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : system name
        /// </remarks>
        private const string ACCOUNTROLES_BY_SYSTEMNAME_KEY = "Ems.accountrole.systemname-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ACCOUNTROLES_PATTERN_KEY = "Ems.accountrole.";

        #endregion

        #region Fields

        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<AccountRole> _accountRoleRepository;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly AccountSettings _accountSettings;
        private readonly CommonSettings _commonSettings;

        #endregion

        #region Ctor

        public AccountService(ICacheManager cacheManager,
            IRepository<Account> accountRepository,
            IRepository<AccountRole> accountRoleRepository,
            IDataProvider dataProvider,
            IDbContext dbContext,
            AccountSettings accountSettings,
            CommonSettings commonSettings)
        {
            this._cacheManager = cacheManager;
            this._accountRepository = accountRepository;
            this._accountRoleRepository = accountRoleRepository;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._accountSettings = accountSettings;
            this._commonSettings = commonSettings;
        }

        #endregion

        #region Methods

        #region Accounts
        
        /// <summary>
        /// Gets all accounts
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="accountRoleIds">A list of account role identifiers to filter by (at least one match); pass null or empty list in order to load all accounts; </param>
        /// <param name="email">Email; null to load all accounts</param>
        /// <param name="username">Username; null to load all accounts</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Account collection</returns>
        public virtual  IPagedList<Account> GetAllAccounts(DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int[] accountRoleIds = null, 
             string email = null, string username = null,string fullname = null,
            int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _accountRepository.Table;
            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
            query = query.Where(c => !c.Deleted);
            if (accountRoleIds != null && accountRoleIds.Length > 0)
                query = query.Where(c => c.AccountRoles.Select(cr => cr.Id).Intersect(accountRoleIds).Any());
            if (!String.IsNullOrWhiteSpace(email))
                query = query.Where(c => c.Email.Contains(email));
            if (!String.IsNullOrWhiteSpace(username))
                query = query.Where(c => c.Username.Contains(username));
            if (!String.IsNullOrWhiteSpace(fullname))
                query = query.Where(c => c.Fullname.Contains(fullname));
         
            
            query = query.OrderByDescending(c => c.CreatedOnUtc);

            var accounts = new PagedList<Account>(query, pageIndex, pageSize);
            return accounts;
        }

     

        /// <summary>
        /// Delete a account
        /// </summary>
        /// <param name="account">Account</param>
        public virtual void DeleteAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            account.Deleted = true;

            UpdateAccount(account);
        }

        /// <summary>
        /// Gets a account
        /// </summary>
        /// <param name="accountId">Account identifier</param>
        /// <returns>A account</returns>
        public virtual Account GetAccountById(int accountId)
        {
            if (accountId == 0)
                return null;
            
            return _accountRepository.GetById(accountId);
        }

        /// <summary>
        /// Get accounts by identifiers
        /// </summary>
        /// <param name="accountIds">Account identifiers</param>
        /// <returns>Accounts</returns>
        public virtual IList<Account> GetAccountsByIds(int[] accountIds)
        {
            if (accountIds == null || accountIds.Length == 0)
                return new List<Account>();

            var query = from c in _accountRepository.Table
                        where accountIds.Contains(c.Id)
                        select c;
            var accounts = query.ToList();
            //sort by passed identifiers
            var sortedAccounts = new List<Account>();
            foreach (int id in accountIds)
            {
                var account = accounts.Find(x => x.Id == id);
                if (account != null)
                    sortedAccounts.Add(account);
            }
            return sortedAccounts;
        }

        /// <summary>
        /// Gets a account by GUID
        /// </summary>
        /// <param name="accountGuid">Account GUID</param>
        /// <returns>A account</returns>
        public virtual Account GetAccountByGuid(Guid accountGuid)
        {
            if (accountGuid == Guid.Empty)
                return null;

            var query = from c in _accountRepository.Table
                        where c.AccountGuid == accountGuid
                        orderby c.Id
                        select c;
            var account = query.FirstOrDefault();
            return account;
        }

        /// <summary>
        /// Get account by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Account</returns>
        public virtual Account GetAccountByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in _accountRepository.Table
                        orderby c.Id
                        where c.Email == email
                        select c;
            var account = query.FirstOrDefault();
            return account;
        }

        /// <summary>
        /// Get account by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Account</returns>
        public virtual Account GetAccountByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _accountRepository.Table
                        orderby c.Id
                        where c.Username == username
                        select c;
            var account = query.FirstOrDefault();
            return account;
        }
     
        /// <summary>
        /// Insert a account
        /// </summary>
        /// <param name="account">Account</param>
        public virtual void InsertAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            _accountRepository.Insert(account);
        }
        
        /// <summary>
        /// Updates the account
        /// </summary>
        /// <param name="account">Account</param>
        public virtual void UpdateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            _accountRepository.Update(account);

        }

       
       

        #endregion
        
        #region Account roles

        /// <summary>
        /// Delete a account role
        /// </summary>
        /// <param name="accountRole">Account role</param>
        public virtual void DeleteAccountRole(AccountRole accountRole)
        {
            if (accountRole == null)
                throw new ArgumentNullException("accountRole");

            _accountRoleRepository.Delete(accountRole);

            _cacheManager.RemoveByPattern(ACCOUNTROLES_PATTERN_KEY);

        }

        /// <summary>
        /// Gets a account role
        /// </summary>
        /// <param name="accountRoleId">Account role identifier</param>
        /// <returns>Account role</returns>
        public virtual AccountRole GetAccountRoleById(int accountRoleId)
        {
            if (accountRoleId == 0)
                return null;

            return _accountRoleRepository.GetById(accountRoleId);
        }


        /// <summary>
        /// Gets all account roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Account role collection</returns>
        public virtual IList<AccountRole> GetAllAccountRoles(bool showHidden = false)
        {
            string key = string.Format(ACCOUNTROLES_ALL_KEY, showHidden);
            return _cacheManager.Get(key, () =>
            {
                var query = from cr in _accountRoleRepository.Table
                            orderby cr.Name
                            where (showHidden || cr.Active)
                            select cr;
                var accountRoles = query.ToList();
                return accountRoles;
            });
        }
        
        /// <summary>
        /// Inserts a account role
        /// </summary>
        /// <param name="accountRole">Account role</param>
        public virtual void InsertAccountRole(AccountRole accountRole)
        {
            if (accountRole == null)
                throw new ArgumentNullException("accountRole");

            _accountRoleRepository.Insert(accountRole);

            _cacheManager.RemoveByPattern(ACCOUNTROLES_PATTERN_KEY);

        }

        /// <summary>
        /// Updates the account role
        /// </summary>
        /// <param name="accountRole">Account role</param>
        public virtual void UpdateAccountRole(AccountRole accountRole)
        {
            if (accountRole == null)
                throw new ArgumentNullException("accountRole");

            _accountRoleRepository.Update(accountRole);

            _cacheManager.RemoveByPattern(ACCOUNTROLES_PATTERN_KEY);

        }

        #endregion

        #endregion
    }
}