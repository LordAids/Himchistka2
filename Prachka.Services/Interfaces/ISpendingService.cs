using Himchistka.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.Interfaces
{
    public interface ISpendingService
    {
        public Task<IList<DTOSpending>> GetAllPlaces();
        public Task<DTOSpending> GetSpendingById(Guid placeId);
        public Task<DTOSpending> UpsertSpending(DTOSpending placeModel);
        public Task DeleteSpending(Guid placeId);
    }
}
