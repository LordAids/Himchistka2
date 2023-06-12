using AutoMapper;
using Himchistka.Data.Entities;
using Himchistka.Services.DTO;

namespace Himchistka.Api.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<Order, DTOUpsertOrder>().ReverseMap();
            CreateMap<Service, DTOServices>().ReverseMap();
            CreateMap<Place, DTOPlace>().ReverseMap();
            CreateMap<Spending, DTOSpending>().ReverseMap();
        }
    }
}
