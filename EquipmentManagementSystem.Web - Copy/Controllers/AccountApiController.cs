using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EquipmentManagementSystem.Web.Models;
using EquipmentManagementSystem.Services.Identity;
using EquipmentManagementSystem.Core.Domain.Identity;
using EquipmentManagementSystem.Data;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace EquipmentManagementSystem.Web.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/accounts")]
    public class AccountApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IAccountService _accountService;

        public AccountApiController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,EmsIdentityDbContext emsIdentityDbContext, IAccountService accountService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _accountService = accountService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [ResponseType(typeof(ApplicationUser))]
        public List<ApplicationUser> GetUsers(string keyword, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return _accountService.GetUsers(null, pageIndex, pageSize).ToList();
        }
    }
}
