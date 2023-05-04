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
        private readonly IMapper _mapper;
        public OrderServices(IConfiguration configuration, ApplicationDbContext db, IMapper mapper)
        {
            _context = db;
            _mapper = mapper;
        }

        public async Task<DTOUpsertOrder> UpsertOrder(DTOUpsertOrder model)
        {
            DTOUpsertOrder res = new();
            if (model.Id == null)
            {
                Order order = new Order
                {
                    ClientId = (Guid)(model.ClientId != null ? model.ClientId : Guid.Empty),
                    Services = await _context.Services.Where(s => model.Services.Contains(s.ServiceId)).ToListAsync(),
                };
                res = _mapper.Map<DTOUpsertOrder>(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == model.Id);
                order.ClientId = (Guid)(model.ClientId != null ? model.ClientId : Guid.Empty);
                order.Services = await _context.Services.Where(s => model.Services.Contains(s.ServiceId)).ToListAsync();
                res = _mapper.Map<DTOUpsertOrder>(order);
            }
            await _context.SaveChangesAsync();
            return res;
        }

    }
}
    