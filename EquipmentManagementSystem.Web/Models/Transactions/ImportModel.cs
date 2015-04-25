using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipmentManagementSystem.Web.Models.Catalog;

namespace EquipmentManagementSystem.Web.Models.Transactions
{
    public class ImportModel
    {
        public ImportModel()
        {
            AvailableWarehouseProfiles = new List<SelectListItem>();
            TransactionDetails = new List<TransactionDetailModel>();
            ImportDate = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public string ImportDate { get; set; }

        [UIHint("RichEditor")]
        public string Note { get; set; }

        public int WarehouseProfileId { get; set; }

        public List<TransactionDetailModel> TransactionDetails { get; set; }

        public List<SelectListItem> AvailableWarehouseProfiles { get; set; }

        public List<CategoryModel> Categories { get; set; }
    }
}