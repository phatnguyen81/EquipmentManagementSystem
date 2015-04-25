using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Framework.Datatables
{
    public class DataSourceRequest
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public int Page { get { return (int)(Start/Length); } }

        public int PageSize { get { return Length; } }

        public DataSourceRequest()
        {
            this.Start = 0;
            this.Length = 10;
        }
    }
}
