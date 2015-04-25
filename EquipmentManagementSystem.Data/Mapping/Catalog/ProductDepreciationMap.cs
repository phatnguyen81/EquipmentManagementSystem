using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Catalog;

namespace EquipmentManagementSystem.Data.Mapping.Catalog
{
    public class ProductDepreciationMap : EntityTypeConfiguration<ProductDepreciation>
    {
        public ProductDepreciationMap()
        {
            this.ToTable("ProductDepreciation");
            this.HasKey(s => s.Id);
            this.Property(s => s.Code).HasMaxLength(20);

            this.HasRequired(pd => pd.Product).WithMany(p => p.ProductDepreciations).HasForeignKey(pd => pd.ProductId);
        }
    }
}
