using EquipmentManagementSystem.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services.Identity
{
    public interface IAccountService
    {
        IList<ApplicationUser> GetAccounts(string keyword, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
