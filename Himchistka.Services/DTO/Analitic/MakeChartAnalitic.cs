using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.DTO.Analitic
{
    public class MakeChartAnalitic
    {
        public List<Guid>? Places { get; set; }
        public List<Guid>? Services { get;set; }
        public List<Guid>? Spendings { get; set; }
        public List<DateTime>? Dates { get; set; }
    }
}
