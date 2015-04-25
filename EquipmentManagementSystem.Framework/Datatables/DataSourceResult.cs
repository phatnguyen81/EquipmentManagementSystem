using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Framework.Datatables
{
    public class DataSourceResult
    {
        public IEnumerable data { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }
    }
}
