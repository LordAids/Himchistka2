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
    public class ClientService : IClientService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClientService(IConfiguration configuration, ApplicationDbContext db, IMapper mapper)
        {
            _context = db;
            _mapper = mapper;
        }

        public async Task DeleteClient(Guid clientId)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
                if (client != null)
                {
                    _context.Clients.Remove(client);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex) { }
        }

        public async Task<IList<DTOClient>> GetAllClients()
        {
            return _mapper.Map<List<DTOClient>>(_context.Clients.AsNoTracking().ToList());
        }

        public async Task<DTOClient> GetClientById(Guid clientId)
        {
            return _mapper.Map<DTOClient>(await _context.Clients.FirstOrDefaultAsync(p => p.Id == clientId));

        }

        public async Task<DTOClient> UpsertClient(DTOClient model)
        {
            try
            {
                if (model.Id == null)
                {
                    Client client = _mapper.Map<Client>(model);
                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();
                    model.Id = client.Id;
                }
                else
                {
                    var client = await _context.Clients.FirstOrDefaultAsync(p => p.Id == model.Id);
                    client.FirstName = model.FirstName;
                    client.LastName = model.LastName;
                    client.Email = model.Email;
                    client.PhoneNumber = model.PhoneNumber;
                    _context.Clients.Update(client);
                    await _context.SaveChangesAsync();
                }

                return model;

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
