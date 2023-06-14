using AutoMapper;
using Himchistka.Data;
using Himchistka.Data.Entities;
using Himchistka.Services.DTO.Analitic;
using Himchistka.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.Services
{
    public class AnaliticService : IAnaliticService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AnaliticService(IConfiguration configuration, ApplicationDbContext db, IMapper mapper)
        {
            _context = db;
            _mapper = mapper;
        }
        public async Task<DTOChartAnalitic> GetChartAnalitic(MakeChartAnalitic model)
        {
            var orders = _context.Orders
                                 .Include(o => o.Services)
                                     .ThenInclude(s => s.Spendings)
                                        .ThenInclude(s => s.Spending)
                                 .Include(o => o.Place);

            var services = _context.Services
                                .Include(s => s.Spendings)
                                .Include(s => s.Spendings);

            var res = new DTOChartAnalitic();
            var labes = new List<string>();
            //прописание labels
            foreach (var order in orders)
                labes.Add(order.CreationTime.Value.ToShortDateString());

            res.Labels.AddRange(labes);
            
            foreach(var label in labes)
            {
                res.Profits.Add(orders.Where(o => o.CreationTime.Value.ToShortDateString() == label).Select(s => s.Cost).Sum());
                res.Spendings.Add(GetOrderSpendings(orders.Where(o => o.CreationTime.Value.ToShortDateString() == label)));

            }


            return res;

        }


        public decimal GetOrderSpendings(IEnumerable<Order> orders)
        {
            decimal res = 0;
            var spendings = _context.Spendings.ToList();
            foreach (var order in orders)
            {
                var services = order.Services;
                var servicesSpendings = _context.SpendingServices.Where(s => services.Select(sr => sr.Id).Contains(s.Id));
                spendings = spendings.Where(s => servicesSpendings.Select(ss => ss.Spending.Id).Contains(s.Id)).ToList();


                foreach (var s in spendings)
                {
                    res += (decimal)(s.Price * servicesSpendings.FirstOrDefault(s => s.Service.Id == s.Id).Count);
                }
            }

            return res;

        }
        public Task<DTOPieAnalitic> GetPieAnalitic(MakePieAnalitic model)
        {
            throw new NotImplementedException();
        }
    }
}
