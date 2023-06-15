using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.DTO.Analitic
{
    public class DTOChartAnalitic
    {
        public List<string> Labels {  get; set; }
        public List<double> Spendings {  get; set; }
        public List<decimal> Profits {  get; set; }

    }
}
