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
    public class SpendingService : ISpendingService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SpendingService(IConfiguration configuration, ApplicationDbContext db, IMapper mapper)
        {
            _context = db;
            _mapper = mapper;
        }

        public async Task<IList<DTOSpending>> GetAllPlaces()
        {
            var Spending = await _context.Spendings.ToListAsync();
            return _mapper.Map<IList<DTOSpending>>(Spending);
        }

        public async Task<DTOSpending> GetSpendingById(Guid spendingId)
        {
            var Spending = _context.Spendings.FirstOrDefault(s => s.Id == spendingId);
            return _mapper.Map<DTOSpending>(Spending);
        }

        public async Task<DTOSpending> UpsertSpending(DTOSpending model)
        {
            DTOSpending res = new();
            if (model.Id == null)
            {
                res = _mapper.Map<DTOSpending>(await _context.Spendings.AddAsync(_mapper.Map<Spending>(model)));
            }
            else
            {
                var service = _context.Spendings.FirstOrDefault(s => s.Id == model.Id);
                service = _mapper.Map<Spending>(model);
                res = _mapper.Map<DTOSpending>(service);
                _context.Spendings.Update(service);
            }
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task DeleteSpending(Guid placeId)
        {
            var spending = await _context.Spendings.FirstOrDefaultAsync(s => s.Id == placeId);
            if (spending != null)
                _context.Spendings.Remove(spending);
            await _context.SaveChangesAsync();
        }
    }
}
