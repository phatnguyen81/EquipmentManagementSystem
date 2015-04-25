using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Core.Domain.Transactions
{
    public class TransactionDetail : BaseEntity
    {
        public int TransactionId { get; set; }

        public int TransactionTypeId { get; set; }

        public int ProductId { get; set; }

        public int ProductDepreciationId { get; set; }

        public decimal Qtty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal PricePerDay { get; set; }

        public Transaction Transaction { get; set; }
        
    }
}
