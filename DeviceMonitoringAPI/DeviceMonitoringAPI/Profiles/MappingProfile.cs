using AutoMapper;
using DeviceMonitoringAPI.DTOs;
using DeviceMonitoringAPI.Models;

namespace DeviceMonitoringAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSessionDto, DeviceSession>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));

            CreateMap<DeviceSession, DeviceSessionDto>();
        }
    }
}
