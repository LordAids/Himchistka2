using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.DTO
{
    public class DTOServices
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<Guid>? Places { get; set; }
        public List<DTOServiceSpending> Spendings { get; set; }
    }

    public class DTOServiceSpending
    {
        public Guid? Id { get; set; }
        public decimal Count { get; set; }
    }
}
