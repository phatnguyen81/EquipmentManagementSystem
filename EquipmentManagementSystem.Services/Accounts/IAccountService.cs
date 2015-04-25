using System;
using System.Collections.Generic;
using EquipmentManagementSystem.Core;
using EquipmentManagementSystem.Core.Domain.Accounts;

namespace EquipmentManagementSystem.Services.Accounts
{
    /// <summary>
    /// Account service interface
    /// </summary>
    public partial interface IAccountService
    {
        #region Accounts

         IPagedList<Account> GetAllAccounts(DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int[] accountRoleIds = null, 
             string email = null, string username = null,string fullname = null,
            int pageIndex = 0, int pageSize = 2147483647); 
        


        /// <summary>
        /// Delete a account
        /// </summary>
        /// <param name="account">Account</param>
        void DeleteAccount(Account account);

        /// <summary>
        /// Gets a account
        /// </summary>
        /// <param name="accountId">Account identifier</param>
        /// <returns>A account</returns>
        Account GetAccountById(int accountId);

        /// <summary>
        /// Get accounts by identifiers
        /// </summary>
        /// <param name="accountIds">Account identifiers</param>
        /// <returns>Accounts</returns>
        IList<Account> GetAccountsByIds(int[] accountIds);

        /// <summary>
        /// Gets a account by GUID
        /// </summary>
        /// <param name="accountGuid">Account GUID</param>
        /// <returns>A account</returns>
        Account GetAccountByGuid(Guid accountGuid);

        /// <summary>
        /// Get account by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Account</returns>
        Account GetAccountByEmail(string email);


        /// <summary>
        /// Get account by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Account</returns>
        Account GetAccountByUsername(string username);

        /// <summary>
        /// Insert a account
        /// </summary>
        /// <param name="account">Account</param>
        void InsertAccount(Account account);

        /// <summary>
        /// Updates the account
        /// </summary>
        /// <param name="account">Account</param>
        void UpdateAccount(Account account);
        
        #endregion

        #region Account roles

        /// <summary>
        /// Delete a account role
        /// </summary>
        /// <param name="accountRole">Account role</param>
        void DeleteAccountRole(AccountRole accountRole);

        /// <summary>
        /// Gets a account role
        /// </summary>
        /// <param name="accountRoleId">Account role identifier</param>
        /// <returns>Account role</returns>
        AccountRole GetAccountRoleById(int accountRoleId);

      
        /// <summary>
        /// Gets all account roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Account role collection</returns>
        IList<AccountRole> GetAllAccountRoles(bool showHidden = false);

        /// <summary>
        /// Inserts a account role
        /// </summary>
        /// <param name="accountRole">Account role</param>
        void InsertAccountRole(AccountRole accountRole);

        /// <summary>
        /// Updates the account role
        /// </summary>
        /// <param name="accountRole">Account role</param>
        void UpdateAccountRole(AccountRole accountRole);

        #endregion
    }
}