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
            CreateMap<Service, DTOServices>().ReverseMap()
                .ForMember(c => c.Spendings, c => c.Ignore())
                .ForMember(c => c.Places, c => c.Ignore());
            CreateMap<Place, DTOPlace>().ReverseMap();
            CreateMap<Spending, DTOSpending>().ReverseMap();
        }
    }
}
