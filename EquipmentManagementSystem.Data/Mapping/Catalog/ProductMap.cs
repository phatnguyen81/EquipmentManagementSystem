using EquipmentManagementSystem.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace EquipmentManagementSystem.Data.Mapping.Catalog
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.ToTable("Product");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(400);
            this.Property(c => c.Sku).IsRequired().HasMaxLength(50);

            this.HasRequired(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
        }
    }
}
