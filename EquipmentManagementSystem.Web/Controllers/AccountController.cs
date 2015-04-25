using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Core.Domain.Accounts;
using EquipmentManagementSystem.Framework.Controllers;
using EquipmentManagementSystem.Services.Accounts;
using EquipmentManagementSystem.Services.Authentication;
using EquipmentManagementSystem.Web.Models.Account;

namespace EquipmentManagementSystem.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Fields
        private readonly IAccountRegistrationService _accountRegistrationService;
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Ctor

        public AccountController(
            IAccountRegistrationService accountRegistrationService, 
            IAccountService accountService, 
            IAuthenticationService authenticationService)
        {
            _accountRegistrationService = accountRegistrationService;
            _accountService = accountService;
            _authenticationService = authenticationService;
        }

        #endregion

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Username))
                {
                    model.Username = model.Username.Trim();
                }
                var loginResult = _accountRegistrationService.ValidateAccount(model.Username, model.Password);
                switch (loginResult)
                {
                    case AccountLoginResults.Successful:
                    {
                        var account = _accountService.GetAccountByUsername(model.Username);

                            //sign in new account
                            _authenticationService.SignIn(account, model.RememberMe);

                            if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                return RedirectToAction("Index", "Home");

                            return Redirect(returnUrl);
                        }
                    case AccountLoginResults.AccountNotExist:
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        break;
                    case AccountLoginResults.Deleted:
                        ModelState.AddModelError("", "Tài khoản đã bị xóa");
                        break;
                    case AccountLoginResults.NotActive:
                        ModelState.AddModelError("", "Tài khoản chưa hoạt động");
                        break;
                    case AccountLoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", "Sai tên đăng nhập hay mật khẩu");
                        break;
                }
            }

            return View(model);

        }

        public ActionResult Logout()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}