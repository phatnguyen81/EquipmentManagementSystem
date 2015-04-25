using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Index(IsUnique = true)] 
        public string Sku { get; set; }

        public bool Deleted { get; set; }

        public Category Category { get; set; }

        public ICollection<ProductDepreciation> ProductDepreciations { get; set; }
    }
}
