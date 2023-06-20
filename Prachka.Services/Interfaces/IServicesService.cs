using Himchistka.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с услугами
    /// </summary>
    public interface IServicesService
    {
        public Task<DTOServices> UpsertService(DTOServices model);
        public Task<IList<DTOServices>> GetAllServices();
        public Task<DTOServices> GetServiceById(Guid serviceId);
        public Task DeleteService(Guid serviceId);
    }
}
