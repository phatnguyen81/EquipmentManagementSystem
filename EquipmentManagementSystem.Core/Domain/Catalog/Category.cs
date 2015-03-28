using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Core.Domain.Catalog
{
    public class Category : BaseEntity
    {
        private ICollection<Product> _products;

        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentCategoryId { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return _products ?? (_products = new List<Product>()); }
            protected set { _products = value; }
        }
    }
}
