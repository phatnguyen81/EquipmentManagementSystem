using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Framework.Mvc;

namespace EquipmentManagementSystem.Web.Models.Catalog
{
    public class CategoryModel : BaseEmsEntityModel
    {
        public CategoryModel()
        {

        }

        [Required]
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public List<ProductModel> Products { get; set; }
    }
}