using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Framework.Mvc;

namespace EquipmentManagementSystem.Web.Models.Account
{
    public class LoginModel : BaseEmsModel
    {
        [AllowHtml]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [AllowHtml]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}