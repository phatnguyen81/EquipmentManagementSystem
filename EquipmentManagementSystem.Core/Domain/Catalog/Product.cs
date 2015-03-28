using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Core.Domain.Catalog
{
    public class Product : BaseEntity
    {
       
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public string Sku { get; set; }

        public int StockQuantity { get; set; }

        public decimal ProductCost { get; set; }

        public decimal Price { get; set; }

        public bool Deleted { get; set; }

        public Category Category { get; set; }
    }
}
