using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EquipmentManagementSystem.Framework.Mvc;

namespace EquipmentManagementSystem.Web.Models.Catalog
{
    public class CategoryListModel : BaseEmsModel
    {
        public string SearchCategoryName { get; set; }
    }
}