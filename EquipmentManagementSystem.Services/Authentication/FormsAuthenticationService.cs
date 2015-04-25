using System;
using System.Web;
using System.Web.Security;
using EquipmentManagementSystem.Core.Domain.Accounts;
using EquipmentManagementSystem.Services.Accounts;

namespace EquipmentManagementSystem.Services.Authentication
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly IAccountService _accountService;
        private readonly AccountSettings _accountSettings;
        private readonly TimeSpan _expirationTimeSpan;

        private Account _cachedAccount;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="accountService">Account service</param>
        /// <param name="accountSettings">Account settings</param>
        public FormsAuthenticationService(HttpContextBase httpContext,
            IAccountService accountService, AccountSettings accountSettings)
        {
            this._httpContext = httpContext;
            this._accountService = accountService;
            this._accountSettings = accountSettings;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }


        public virtual void SignIn(Account account, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
               account.Username,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                account.Username,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedAccount = account;
        }

        public virtual void SignOut()
        {
            _cachedAccount = null;
            FormsAuthentication.SignOut();
        }

        public virtual Account GetAuthenticatedAccount()
        {
            if (_cachedAccount != null)
                return _cachedAccount;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var account = GetAuthenticatedAccountFromTicket(formsIdentity.Ticket);
            if (account != null && account.Active && !account.Deleted)
                _cachedAccount = account;
            return _cachedAccount;
        }

        public virtual Account GetAuthenticatedAccountFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var usernameOrEmail = ticket.UserData;

            if (String.IsNullOrWhiteSpace(usernameOrEmail))
                return null;
            var account = _accountService.GetAccountByUsername(usernameOrEmail);
            return account;
        }
    }
}