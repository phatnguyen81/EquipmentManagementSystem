using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core;
using EquipmentManagementSystem.Core.Domain.Warehouses;

namespace EquipmentManagementSystem.Services.Warehouses
{
    public interface IWarehouseProfileService
    {
        IPagedList<WarehouseProfile> GetAllWarehouseProfiles(string keyword = "",
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
