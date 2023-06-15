using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.DTO.Analitic
{
    public class DTOPieAnalitic
    {
        public List<string> Labels { get; set; }
        public List<string> Colors { get; set; }
        public List<double> Value { get; set; }
    }
}
