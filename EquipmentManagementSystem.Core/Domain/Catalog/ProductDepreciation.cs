using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Warehouses;

namespace EquipmentManagementSystem.Core.Domain.Catalog
{
    public class ProductDepreciation : BaseEntity
    {
        [Index(IsUnique = true)] 
        public string Code { get; set; }
        
        public int ProductId { get; set; }

        public string TransactionCode { get; set; }
        
        public DateTime ImportDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal ValuePerDay { get; set; }

        public Product Product { get; set; }

    }
}
