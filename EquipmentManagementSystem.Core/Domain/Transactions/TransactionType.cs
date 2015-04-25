using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Core.Domain.Transactions
{
    public class TransactionType : BaseEntity
    {
        [Index(IsUnique = true)] 
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
