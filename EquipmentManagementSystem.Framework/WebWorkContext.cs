using System;
using System.Linq;
using System.Web;
using EquipmentManagementSystem.Core;
using EquipmentManagementSystem.Core.Domain.Accounts;
using EquipmentManagementSystem.Services.Accounts;
using EquipmentManagementSystem.Services.Authentication;

namespace EquipmentManagementSystem.Framework
{
    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string AccountCookieName = "Ems.account";

        #endregion

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;

        private Account _cachedAccount;

        #endregion

        #region Ctor

        public WebWorkContext(HttpContextBase httpContext,
            IAccountService accountService,
            IAuthenticationService authenticationService
            )
        {
            this._httpContext = httpContext;
            this._accountService = accountService;
            this._authenticationService = authenticationService;
        }

        #endregion

        #region Utilities

        protected virtual HttpCookie GetAccountCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[AccountCookieName];
        }

        protected virtual void SetAccountCookie(Guid accountGuid)
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                var cookie = new HttpCookie(AccountCookieName);
                cookie.HttpOnly = true;
                cookie.Value = accountGuid.ToString();
                if (accountGuid == Guid.Empty)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 24*365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }

                _httpContext.Response.Cookies.Remove(AccountCookieName);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current account
        /// </summary>
        public virtual Account CurrentAccount
        {
            get
            {
                if (_cachedAccount != null)
                    return _cachedAccount;

                Account account = null;

                //registered user
                account = _authenticationService.GetAuthenticatedAccount();

                //validation
                if (!account.Deleted && account.Active)
                {
                    SetAccountCookie(account.AccountGuid);
                    _cachedAccount = account;
                }

                return _cachedAccount;
            }
            set
            {
                SetAccountCookie(value.AccountGuid);
                _cachedAccount = value;
            }
        }

    
        #endregion
    }
}
