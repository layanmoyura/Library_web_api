using AutoMapper;
using WebApplication2.Model;

namespace WebApplication2
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRegisterModel, Customer>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(m => m.Email));
        }
    }
}