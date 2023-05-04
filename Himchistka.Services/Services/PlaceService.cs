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
    /// Сервис для работы с точками
    /// </summary>
    public class PlaceService : IPlaceService
    {
        public Task DeletePlace(Guid placeId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<DTOPlace>> GetAllPlaces()
        {
            throw new NotImplementedException();
        }

        public Task<DTOPlace> GetPlaceById(Guid placeId)
        {
            throw new NotImplementedException();
        }

        public Task<DTOPlace> UpsertPlace(DTOPlace placeModel)
        {
            throw new NotImplementedException();
        }
    }
}
