using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Warehouses;

namespace EquipmentManagementSystem.Data.Mapping.Warehouses
{
    public class WarehouseMap : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            this.ToTable("WarehouseMap");
            this.HasKey(s => s.Id);
            this.Property(s => s.Code).IsRequired().HasMaxLength(20);

            this.HasRequired(w => w.ProductDepreciation)
                .WithMany()
                .HasForeignKey(w => w.ProductDepreciationId);

            this.HasRequired(w => w.WarehouseProfile)
                .WithMany()
                .HasForeignKey(w => w.WarehouseProfileId);
        }
    }
}
