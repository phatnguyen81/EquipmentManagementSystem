using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Catalog;

namespace EquipmentManagementSystem.Core.Domain.Warehouses
{
    public class Warehouse : BaseEntity
    {
        [Index(IsUnique = true)] 
        public string Code { get; set; }

        public int WarehouseProfileId { get; set; }

        public int ProductDepreciationId { get; set; }

        public int Qtty { get; set; }

        public ProductDepreciation ProductDepreciation { get; set; }

        public WarehouseProfile WarehouseProfile { get; set; }
    }
}
