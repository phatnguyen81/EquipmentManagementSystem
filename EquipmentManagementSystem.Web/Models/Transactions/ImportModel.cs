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
            TxDateFrom = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public string TransactionTypeCode { get; set; }

        public string TxDateFrom { get; set; }

        public string TxDateTo { get; set; }

        public int AccountId { get; set; }

        [UIHint("RichEditor")]
        public string Note { get; set; }

        public int FromWarehouseProfileId { get; set; }

        public int ToWarehouseProfileId { get; set; }

        public List<TransactionDetailModel> TransactionDetails { get; set; }

        public List<SelectListItem> AvailableWarehouseProfiles { get; set; }

        public List<CategoryModel> Categories { get; set; }
    }
}