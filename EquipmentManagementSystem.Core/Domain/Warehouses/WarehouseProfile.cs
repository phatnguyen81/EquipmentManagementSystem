using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagementSystem.Core.Domain.Warehouses
{
    public class WarehouseProfile : BaseEntity
    {
        [Index(IsUnique = true)]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public ICollection<Warehouse> Warehouses { get; set; }
    }
}
