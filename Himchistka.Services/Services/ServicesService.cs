using Himchistka.Services.DTO;
using Himchistka.Services.Interfaces;
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
        public Task DeleteService(Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<DTOServices>> GetAllServices()
        {
            throw new NotImplementedException();
        }

        public Task<DTOServices> GetServiceById(Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public Task<DTOServices> UpsertService(DTOServices model)
        {
            throw new NotImplementedException();
        }
    }
}
