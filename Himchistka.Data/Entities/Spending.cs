using Himchistka.Data.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Data.Entities
{
    public class Spending
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public double Price { get; set; }

        public virtual List<SpendingServices> Services { get; set; }
    }
}
