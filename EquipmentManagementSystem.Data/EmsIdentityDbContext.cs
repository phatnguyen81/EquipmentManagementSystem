using EquipmentManagementSystem.Core.Data;
using EquipmentManagementSystem.Core.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Data
{
    public class EmsIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public EmsIdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString, throwIfV1Schema: false)
        {
        }

        public static EmsIdentityDbContext Create()
        {
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            return new EmsIdentityDbContext(dataProviderSettings.DataConnectionString);
        }
    }
}
