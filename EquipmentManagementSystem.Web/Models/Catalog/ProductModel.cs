using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Framework.Mvc;

namespace EquipmentManagementSystem.Web.Models.Catalog
{
    public class ProductModel : BaseEmsEntityModel
    {
        public ProductModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Sku { get; set; }
        
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<SelectListItem> AvailableCategories { get; set; }

    }
}