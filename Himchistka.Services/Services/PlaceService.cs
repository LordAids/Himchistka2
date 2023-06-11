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
    /// Сервис для работы с точками
    /// </summary>
    public class PlaceService : IPlaceService
    {

        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PlaceService(IConfiguration configuration, ApplicationDbContext db, IMapper mapper)
        {
            _context = db;
            _mapper = mapper;
        }
        public async Task DeletePlace(Guid placeId)
        {
            try
            {
                var place = await _context.Places.FirstOrDefaultAsync();
                if(place != null) 
                    _context.Places.Remove(place) ; 
            }
            catch (Exception ex) { }
        }

        public async Task<IList<DTOPlace>> GetAllPlaces()
        {
            return _mapper.Map<List<DTOPlace>>(_context.Places.AsNoTracking().ToList());
        }

        public async Task<DTOPlace> GetPlaceById(Guid placeId)
        {
            return _mapper.Map<DTOPlace>(await _context.Places.FirstOrDefaultAsync(p => p.Id == placeId));
        }

        public async Task<DTOPlace> UpsertPlace(DTOPlace placeModel)
        {
            try
            {
                if(placeModel.Id == null)
                {
                    Place place = _mapper.Map<Place>(placeModel);
                    _context.Places.Add(place);
                    await _context.SaveChangesAsync();
                    placeModel.Id = place.Id;
                }
                else
                {
                    var place = await _context.Places.FirstOrDefaultAsync(p => p.Id == placeModel.Id);
                    place.Name = placeModel.Name;
                    place.Adress = placeModel.Adress;
                    _context.Places.Update(place);
                    await _context.SaveChangesAsync();
                }
                
                return placeModel;

            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
