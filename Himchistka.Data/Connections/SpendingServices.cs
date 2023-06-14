using Himchistka.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Data.Connections
{
    public class SpendingServices
    {
        public Guid Id { get; set; }
        public Spending Spending { get; set; }
        public Service Service { get; set; }
        public double Count { get; set; }
    }
}
