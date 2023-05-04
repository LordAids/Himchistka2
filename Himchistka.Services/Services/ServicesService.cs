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
    /// <summary>
    /// Сервис для работы с услугами
    /// </summary>
    public class ServicesService : IServicesService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ServicesService(IConfiguration configuration, ApplicationDbContext db, IMapper mapper)
        {
            _context = db;
            _mapper = mapper;
        }
        public async Task DeleteService(Guid serviceId)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id== serviceId);
            if(service != null) 
                _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<DTOServices>> GetAllServices()
        {
            var Services = await _context.Services.ToListAsync();
            return _mapper.Map<IList<DTOServices>>(Services);
        }

        public async Task<DTOServices> GetServiceById(Guid serviceId)
        {
            var Services = _context.Services.FirstOrDefault(s => s.Id == serviceId);
            return _mapper.Map<DTOServices>(Services);
        }

        public async  Task<DTOServices> UpsertService(DTOServices model)
        {
            DTOServices res = new();
            if (model.ServiceId == null)
            {
               res =  _mapper.Map<DTOServices>(await _context.Services.AddAsync(_mapper.Map<Service>(model)));
            }
            else
            {
                var service = _context.Services.FirstOrDefault(s => s.Id == model.ServiceId);
                service = _mapper.Map<Service>(model);
                res = _mapper.Map<DTOServices>(service);
                _context.Services.Update(service);
            }
            await _context.SaveChangesAsync();
            return res;
        }
    }
}
