using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Core.Domain.Transactions
{
    public class Transaction : BaseEntity 
    {
        [Index(IsUnique = true)] 
        public string Code { get; set; }

        public string TransactionTypeCode { get; set; }

        public int FromWarehouseProfileId { get; set; }

        public int ToWarehouseProfileId { get; set; }

        public DateTime TxDateFrom { get; set; }

        public DateTime? TxDateTo { get; set; }

        public int AccountIdFrom { get; set; }

        public int? AccountIdTo { get; set; }

        public DateTime CreatedDateFrom { get; set; }

        public DateTime CreatedDateTo { get; set; }

        public string Note { get; set; }

        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
