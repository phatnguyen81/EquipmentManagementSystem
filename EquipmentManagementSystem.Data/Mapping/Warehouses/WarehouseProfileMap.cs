using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Warehouses;

namespace EquipmentManagementSystem.Data.Mapping.Warehouses
{
    public class WarehouseProfileMap : EntityTypeConfiguration<WarehouseProfile>
    {
        public WarehouseProfileMap()
        {
            this.ToTable("WarehouseProfile");
            this.HasKey(s => s.Id);
            this.Property(s => s.Code).IsRequired().HasMaxLength(20);
            this.Property(s => s.Name).HasMaxLength(500);
            this.Property(s => s.Description).HasMaxLength(1000);
            this.Property(s => s.Address).HasMaxLength(500);
        }
    }
}
