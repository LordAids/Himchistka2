using AutoMapper;
using Himchistka.Data;
using Himchistka.Data.Entities;
using Himchistka.Services.DTO.Analitic;
using Himchistka.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Emit;
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
            try
            {
                var maxDate = model.Dates.Max();
                var minDate = model.Dates.Min();
                var orders = _context.Orders
                                  .AsNoTracking()
                                  .Include(o => o.Services)
                                      .ThenInclude(s => s.Spendings)
                                         .ThenInclude(s => s.Spending)
                                  .Include(o => o.Place)
                                  .Where(p => model.Places.Contains(p.Place.Id)
                                   && (p.CreationTime > minDate.AddDays(-1) && p.CreationTime < maxDate.AddDays(1)))
                                  .ToList();

                var services = _context.Services
                                    .AsNoTracking()
                                    .Include(s => s.Spendings).ToList();

                var res = new DTOChartAnalitic()
                {
                    Labels = new List<string>(),
                    Profits = new List<decimal>(),
                    Spendings = new List<double>()
                };
                var labes = new List<DateTime>();
                //прописание labels
                foreach (var order in orders)
                    if (order.CreationTime != null)
                    {
                        labes.Add(order.CreationTime.Value.Date);
                        res.Labels.Add(order.CreationTime.Value.ToShortDateString());
                    }

                labes = labes.Distinct().OrderBy(d => d).ToList();
                res.Labels = res.Labels.Distinct().OrderBy(d => d).ToList();


                for(int i =  0; i < labes.Count; i++)
                {
                    var labelOrders = orders.Where(o => o.CreationTime != null && o.CreationTime.Value.Date == labes[i]);
                    //res.Profits.Add(labelOrders.Where(s => model.Services.Intersect(s.Services.Select(s => s.Id)).Any()).Select(s => s.Cost).Sum());
                    var profitServicesForOrder = labelOrders.Select(s => s.Services).ToList();
                    List<Service> profitServices = new List<Service>();
                    foreach (var service in profitServicesForOrder)
                        foreach (var s in service)
                            if (model.Services.Contains(s.Id))
                                profitServices.Add(s);

                    res.Profits.Add(profitServices.Select(s => s.Price).Sum());
                    res.Spendings.Add(GetOrderSpendings(labelOrders, model));

                    res.Spendings[i] += GetPlaceSpendings(model);
                }

                /*foreach (var label in labes)
                {
                    var labelOrders = orders.Where(o => o.CreationTime!= null && o.CreationTime.Value.Date == label);
                    //res.Profits.Add(labelOrders.Where(s => model.Services.Intersect(s.Services.Select(s => s.Id)).Any()).Select(s => s.Cost).Sum());
                    var profitServicesForOrder = labelOrders.Select(s => s.Services).ToList();
                    List<Service> profitServices= new List<Service>();
                    foreach(var service in profitServicesForOrder)
                        foreach(var s in service)
                            if (model.Services.Contains(s.Id))
                                profitServices.Add(s);

                    res.Profits.Add(profitServices.Select(s => s.Price).Sum());
                    res.Spendings.Add(GetOrderSpendings(labelOrders, model));
                }*/


                return res;
            }
            catch (Exception ex)
            {
                throw new Exception (ex.ToString());
            }

        }

        private double GetPlaceSpendings(MakeChartAnalitic model)
        {

            var places = _context.Places.Where(p => model.Places.Contains(p.Id))
                                        .ToList();

            double res = 0;
            //double dayCount = (model.Dates.Max() - model.Dates.Min()).TotalDays;
            foreach (var place in places)
                res += (place.MounthPrice / 30);

            return res;
        }

        private double GetOrderSpendings(IEnumerable<Order> orders, MakeChartAnalitic model)
        {
            double res = 0;

            var services = _context.Services.Include(s => s.Spendings)
                                            .ThenInclude(s => s.Spending)
                                            .Include(o => o.Orders)
                                            .ToList();
            //var _spendingServices = _context.SpendingServices.AsNoTracking().ToList();
            foreach (var order in orders)
            {
                var orderService = services.Where(s => s.Orders.Select(s => s.Id).Contains(order.Id)).ToList();
                var orderSpendings = orderService.Select(s => s.Spendings).ToList();

                foreach(var ord in orderSpendings)
                    foreach(var s in ord.Select(s => s.Spending))
                        if(model.Spendings.Contains(s.Id))
                        {
                            var pr = s.Price;
                            var sum = ord.Where(ss => ss.Spending == s).Select(s => s.Count).Sum();
                            res += s.Price * ord.FirstOrDefault(sp => sp.Spending.Id == s.Id).Count;
                        }

            }

            return res;

        }

        public async Task<DTOPieAnalitic> GetPieAnalitic(MakeChartAnalitic model)
        {
            var maxDate = model.Dates.Max();
            var minDate = model.Dates.Min();
            var res = new DTOPieAnalitic()
            {
                Labels = new List<string>(),
                Colors = new List<string>(),
                Value = new List<double>()
            };

            var orders = _context.Orders
                                 .AsNoTracking()
                                 .Include(o => o.Services)
                                     .ThenInclude(s => s.Spendings)
                                        .ThenInclude(s => s.Spending)
                                 .Include(o => o.Place)
                                 .Where(p => model.Places.Contains(p.Place.Id)
                                  && (p.CreationTime > minDate.AddDays(-1) && p.CreationTime < maxDate.AddDays(1)))
                                 .ToList();

            var services = orders.SelectMany(s => s.Services).Where(s => model.Services.Contains(s.Id)).ToList();
            var dataServices = _context.Services.ToList();
            foreach(var s in dataServices.Where(sr => services.Select(s => s.Id).Contains(sr.Id)))
            {
                res.Labels.Add(s.Name);
                res.Colors.Add(s.Color);
                res.Value.Add((double)services.Where(sr => sr.Id == s.Id).Select(sr => sr.Price).Sum());
            }

            var spendings = orders.SelectMany(s => s.Services).SelectMany(s => s.Spendings).Select(s => s.Spending).ToList();
            var dataSpendings = _context.Spendings.ToList();
            var dataServiceSpendings = _context.SpendingServices.Include(s => s.Service).ToList();

            List<Guid> serviceInAnalytic = new List<Guid>();

            foreach(var s in dataSpendings.Where(sp => spendings.Select(s => s.Id).Contains(sp.Id)))
            {
                res.Labels.Add(s.Name);
                res.Colors.Add(s.Color);
                res.Value.Add((double)spendings.Where(sr => sr.Id == s.Id).Select(sr => sr.Price).Sum() * dataServiceSpendings.FirstOrDefault(sp => sp.Spending == s && !serviceInAnalytic.Contains(sp.Service.Id)).Count
                               );
                serviceInAnalytic.Add(s.Id);
            }


            return res;
        }
    }
}
