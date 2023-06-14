using Himchistka.Services.DTO.Analitic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.Interfaces
{
    public interface IAnaliticService
    {
        public Task<DTOChartAnalitic> GetChartAnalitic(MakeChartAnalitic model);
        public Task<DTOPieAnalitic> GetPieAnalitic(MakePieAnalitic model);
    }
}
