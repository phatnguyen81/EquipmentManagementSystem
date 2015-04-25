using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Framework.Mvc;

namespace EquipmentManagementSystem.Web.Models.Catalog
{
    public class ProductListModel : BaseEmsModel
    {
        public ProductListModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }
        public string SearchKeyword { get; set; }

        public int? SearchCategoryId{ get; set; }

        public List<SelectListItem> AvailableCategories { get; set; }
    }
}