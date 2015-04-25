using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core;
using EquipmentManagementSystem.Core.Data;
using EquipmentManagementSystem.Core.Domain.Warehouses;

namespace EquipmentManagementSystem.Services.Warehouses
{
    public class WarehouseProfileService : IWarehouseProfileService
    {
        private readonly IRepository<WarehouseProfile> _warehouseProfileRepository;

        public WarehouseProfileService(IRepository<WarehouseProfile> warehouseProfileRepository)
        {
            _warehouseProfileRepository = warehouseProfileRepository;
        }

        public IPagedList<WarehouseProfile> GetAllWarehouseProfiles(string keyword = "", int pageIndex = 0, int pageSize = Int32.MaxValue)
        {
            var query = _warehouseProfileRepository.Table;
            if (!String.IsNullOrWhiteSpace(keyword))
                query = query.Where(c => c.Name.Contains(keyword) || c.Code.Contains(keyword));

            //only distinct categories (group by ID)
            query = from c in query
                    group c by c.Id
                        into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault();
            query = query.OrderBy(c => c.Id);

            //paging
            return new PagedList<WarehouseProfile>(query, pageIndex, pageSize);
        }
    }
}
