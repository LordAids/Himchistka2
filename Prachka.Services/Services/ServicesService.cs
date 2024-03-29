﻿using AutoMapper;
using Himchistka.Data;
using Himchistka.Data.Connections;
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
            var Services = await _context.Services.Include(p => p.Places).ToListAsync();
            var res = _mapper.Map<IList<DTOServices>>(Services);

            foreach(var service in res)
            {
                service.Places = new List<Guid>();
                var serv = Services.FirstOrDefault(s => s.Id == service.Id);
                var places = serv.Places.Select(s => s.Id).ToList();
                service.Places.AddRange(places);

            }

            return res;
        }

        public async Task<DTOServices> GetServiceById(Guid serviceId)
        {
            var Services = _context.Services.FirstOrDefault(s => s.Id == serviceId);
            return _mapper.Map<DTOServices>(Services);
        }

        public async  Task<DTOServices> UpsertService(DTOServices model)
        {
            DTOServices res = new();
            Service service = new();
            List<Place> places = new List<Place>();
            if(model.Places != null) places = await _context.Places.Where(p => model.Places.Contains(p.Id)).ToListAsync();

            List<SpendingServices> spendings = new List<SpendingServices>();
            
            if (model.Id == null)
            {
                service = _mapper.Map<Service>(model);
                service.Places = places;
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                model.Id = service.Id;
            }
            else
            {
                service = _context.Services.FirstOrDefault(s => s.Id == model.Id);
                if(service == null)
                {
                    service.Places = places;
                    service = _mapper.Map<Service>(model);
                    res = _mapper.Map<DTOServices>(service);
                    _context.Services.Update(service);
                }
                
            }

            if (model.Spendings != null)
            {
                var spending = _context.Spendings.Where(s => model.Spendings.Select(sp => sp.Id).Contains(s.Id));
                foreach (var sp in spending)
                {
                    spendings.Add(new SpendingServices()
                    {
                        Spending = sp,
                        Service = service,
                        Count = (double)(model.Spendings.FirstOrDefault(s => s.Id == sp.Id) != null ? model.Spendings.FirstOrDefault(s => s.Id == sp.Id).Count : 1),
                    });
                }
                _context.SpendingServices.AddRange(spendings);
            }
            

            await _context.SaveChangesAsync();
            return res;
        }
    }
}
