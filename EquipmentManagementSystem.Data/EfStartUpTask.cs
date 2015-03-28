using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManagementSystem.Core.Infrastructure;
using EquipmentManagementSystem.Core.Data;

namespace EquipmentManagementSystem.Data
{
    public class EfStartUpTask : IStartupTask
    {
        public int Order
        {
            get { return -1000; }
        }

        public void Execute()
        {
            var settings = EngineContext.Current.Resolve<DataSettings>();
            if (settings != null && settings.IsValid())
            {
                var provider = EngineContext.Current.Resolve<IDataProvider>();
                if (provider == null)
                    throw new Exception("No IDataProvider found");
                provider.SetDatabaseInitializer();
            }
        }
    }
}
