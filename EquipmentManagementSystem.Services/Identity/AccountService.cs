using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Domain.Identity;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Core;

namespace EquipmentManagementSystem.Services.Identity
{
    public class AccountService : IAccountService
    {
        private EmsIdentityDbContext _emsIdentityDbContext;

        public AccountService(EmsIdentityDbContext emsIdentityDbContext)
        {
            _emsIdentityDbContext = emsIdentityDbContext;
        }
        public IList<ApplicationUser> GetUsers(string keyword, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _emsIdentityDbContext.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(q => q.UserName.Contains(keyword) || q.Email.Contains(keyword));
            }
            var accounts = new PagedList<ApplicationUser>(query, pageIndex, pageSize);

            return accounts;
        }
    }
}
