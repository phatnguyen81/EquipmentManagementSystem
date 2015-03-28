using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EquipmentManagementSystem.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace EquipmentManagementSystem.Data.Mapping.Catalog
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable("Category");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(400);

            this.HasMany(c => c.Products).WithRequired(p => p.Category).HasForeignKey(p => p.CategoryId);
        }
        
    }
}
