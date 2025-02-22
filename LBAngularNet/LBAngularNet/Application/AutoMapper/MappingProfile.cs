using AutoMapper;
using LBAngularNet.Application.DTO;
using LBAngularNet.Core.Domain.Entities;

namespace LBAngularNet.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Demo, DemoDTO>().ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.Birth.Year));
        } 
    }
}
