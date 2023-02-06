using AutoMapper;
using LogService2023.App.Dtos;
using LogService2023.App.Models;

namespace LogService2023.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLogDto, Log>()
                .ForMember(x => x.TimeStamp, o => o.MapFrom(s => DateTimeOffset.Now));
        }
    }
}
