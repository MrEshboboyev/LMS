using AutoMapper;
using LMS.Services.GroupAPI.Models;
using LMS.Services.GroupAPI.Models.Dto;

namespace LMS.Services.GroupAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Group, GroupDto>().ReverseMap();
            });

            return mappingConfig;   
        }
    }
}
