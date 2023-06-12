using AutoMapper;
using Himchistka.Data.Entities;
using Himchistka.Services.DTO;

namespace Himchistka.Api.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<Order, DTOOrders>()
                .ForMember(s => s.Services, s => s.Ignore());
            CreateMap<DTOOrders, Order>()
                .ForMember(s => s.Services, s => s.Ignore());
            CreateMap<Service, DTOServices>().ReverseMap()
                .ForMember(c => c.Spendings, c => c.Ignore())
                .ForMember(c => c.Places, c => c.Ignore());
            CreateMap<Place, DTOPlace>().ReverseMap();
            CreateMap<Spending, DTOSpending>().ReverseMap();
            CreateMap<Client, DTOClient>().ReverseMap();
        }
    }
}
