using Himchistka.Services.DTO;
using Prachka.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.Interfaces
{
    public interface IClientService
    {
        public Task<DTOClient> UpsertClient(DTOClient model);
        public Task<IList<DTOClient>> GetAllClients();
        public Task<DTOClient> GetClientById(Guid orderId);
        public Task DeleteClient(Guid orderId);
        public Task SendMessages(DTOMessages model);
    }
}
