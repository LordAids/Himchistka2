using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.DTO
{
    public class DTOSpending
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public string Color { get; set; } = "#F44336";
        public double Price { get; set; }
    }
}
