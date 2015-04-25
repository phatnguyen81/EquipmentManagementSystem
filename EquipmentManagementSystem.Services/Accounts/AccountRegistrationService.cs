using System;
using EquipmentManagementSystem.Core;
using EquipmentManagementSystem.Core.Domain.Accounts;
using EquipmentManagementSystem.Services.Security;

namespace EquipmentManagementSystem.Services.Accounts
{
    /// <summary>
    /// Account registration service
    /// </summary>
    public class AccountRegistrationService : IAccountRegistrationService
    {
        #region Fields

        private readonly IAccountService _accountService;
        private readonly IEncryptionService _encryptionService;
        private readonly AccountSettings _accountSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="accountService">Account service</param>
        /// <param name="encryptionService">Encryption service</param>
        /// <param name="accountSettings">Account settings</param>
        public AccountRegistrationService(IAccountService accountService, 
            IEncryptionService encryptionService, 
            AccountSettings accountSettings)
        {
            _accountService = accountService;
            _encryptionService = encryptionService;
            _accountSettings = accountSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate account
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual AccountLoginResults ValidateAccount(string usernameOrEmail, string password)
        {
            Account account;
            account = _accountService.GetAccountByUsername(usernameOrEmail);

            if (account == null)
                return AccountLoginResults.AccountNotExist;
            if (account.Deleted)
                return AccountLoginResults.Deleted;
            if (!account.Active)
                return AccountLoginResults.NotActive;

            var pwd = _encryptionService.CreatePasswordHash(password, account.PasswordSalt, "SHA1");

            bool isValid = pwd == account.Password;
            if (!isValid)
                return AccountLoginResults.WrongPassword;

            //save last login date
            account.LastLoginDateUtc = DateTime.UtcNow;
            _accountService.UpdateAccount(account);
            return AccountLoginResults.Successful;
        }

        /// <summary>
        /// Register account
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual AccountRegistrationResult RegisterAccount(AccountRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Account == null)
                throw new ArgumentException("Can't load current account");

            var result = new AccountRegistrationResult();
           
            if (String.IsNullOrEmpty(request.Email))
            {
                result.AddError("Chưa nhập email");
                return result;
            }
            if (!CommonHelper.IsValidEmail(request.Email))
            {
                result.AddError("Email sai cấu trúc");
                return result;
            }
            if (String.IsNullOrWhiteSpace(request.Password))
            {
                result.AddError("Chưa nhập mật khẩu");
                return result;
            }
            if (String.IsNullOrEmpty(request.Username))
            {
                result.AddError("Chưa nhập tên đăng nhập");
                return result;
            }

            //validate unique user
            if (_accountService.GetAccountByEmail(request.Email) != null)
            {
                result.AddError("Email đã tồn tại");
                return result;
            }
            if (_accountService.GetAccountByUsername(request.Username) != null)
            {
                result.AddError("Tên đăng nhập đã tồn tại");
                return result;
            }

            //at this point request is valid
            request.Account.Username = request.Username;
            request.Account.Email = request.Email;

            string saltKey = _encryptionService.CreateSaltKey(5);
            request.Account.PasswordSalt = saltKey;
            request.Account.Password = _encryptionService.CreatePasswordHash(request.Password, saltKey, "SHA1");

            request.Account.Active = request.IsApproved;
            
            _accountService.UpdateAccount(request.Account);
            return result;
        }
        
     
        #endregion
    }
}