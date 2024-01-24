using AutoMapper;
using LMS.Services.Shared.Models;
using LMS.Services.Shared.Models.Dto;

namespace LMS.Services.Shared
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<GroupSubject, GroupSubjectDto>().ReverseMap();
            });

            return mappingConfig;   
        }
    }
}
