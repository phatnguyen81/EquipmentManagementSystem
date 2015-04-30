using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquipmentManagementSystem.Web.Models.Transactions
{
    public class TransactionDetailModel
    {
        public int ProductId { get; set; }

        public decimal Qtty { get; set; }
        public decimal QttyMask { get; set; }

        public decimal UnitPrice { get; set; }

        public string ExpireDate { get; set; }

        public decimal PricePerDay { get; set; }
    }
}