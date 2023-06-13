using AutoMapper;
using Himchistka.Data;
using Himchistka.Data.Entities;
using Himchistka.Services.DTO;
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
    public class OrderServices : IOrderServices
    {
        private ApplicationDbContext _context;
        private IClientService _clientService;
        private readonly IMapper _mapper;
        public OrderServices(IConfiguration configuration, ApplicationDbContext db, IMapper mapper, IClientService clientService)
        {
            _context = db;
            _mapper = mapper;
            _clientService = clientService;
        }

        public async Task<DTOOrders> UpsertOrder(DTOOrders model)
        {
            DTOOrders res = new();
            if (model.Id == null)
            {
                Order order = new Order
                {
                    ClientId = (Guid)(model.ClientId != null ? model.ClientId : Guid.Empty),
                    Services = await _context.Services.Where(s => model.Services.Contains(s.Id)).ToListAsync(),
                    Place = await _context.Places.FirstOrDefaultAsync(s => s.Id == model.PlaceId),
                    Cost = (decimal)model.Cost,
                    Comment = model.Comment,
                    Status = (int)OrderStatus.New
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                res = _mapper.Map<DTOOrders>(order);

            }
            else
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == model.Id);
                order.ClientId = (Guid)(model.ClientId != null ? model.ClientId : Guid.Empty);
                order.Place = await _context.Places.FirstOrDefaultAsync(s => s.Id == model.PlaceId);
                order.Services = await _context.Services.Where(s => model.Services.Contains(s.Id)).ToListAsync();
                order.Comment = model.Comment;
                order.Status = (int)model.Status;
                res = _mapper.Map<DTOOrders>(order);
            }
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<IList<DTOOrders>> GetAllOrders(Guid placeId)
        {
            var orders = _context.Orders.AsNoTracking()
                                        .Include(c => c.Client)
                                        .Include(c => c.Services)
                                        .Include(c => c.Place)
                                        .Where(p => p.Place.Id == placeId)
                                        .ToList();

            var res = new List<DTOOrders>();
            foreach (var order in orders)
            {
                res.Add(new DTOOrders()
                {
                    ClientId = order.ClientId,
                    Services = order.Services.Select(o => o.Id).ToList(),
                    Comment = order.Comment,
                    Cost = order.Cost,
                    Status = order.Status,
                    ClientName = order.Client.FirstName + " " + order.Client.LastName
                });
            }

            return _mapper.Map<List<DTOOrders>>(res);

        }

        public async Task<DTOOrders> GetOrderById(Guid orderId)
        {
            return _mapper.Map<DTOOrders>(await _context.Orders.FirstOrDefaultAsync(p => p.Id == orderId));

        }

        public async Task DeleteOrder(Guid orderId)
        {
            try
            {
                var Order = await _context.Orders.FirstOrDefaultAsync(p => p.Id == orderId);
                if (Order != null)
                {
                    _context.Orders.Remove(Order);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex) { }
        }

    }
}
    