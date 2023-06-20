using Himchistka.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с точками
    /// </summary>
    public interface IPlaceService
    {
        public Task<IList<DTOPlace>> GetAllPlaces();
        public Task<DTOPlace> GetPlaceById(Guid placeId);
        public Task<DTOPlace> UpsertPlace(DTOPlace placeModel);
        public Task DeletePlace (Guid placeId);

    }
}
