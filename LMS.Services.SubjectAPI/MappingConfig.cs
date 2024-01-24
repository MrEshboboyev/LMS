using AutoMapper;
using LMS.Services.SubjectAPI.Models;
using LMS.Services.SubjectAPI.Models.Dto;

namespace LMS.Services.SubjectAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Subject, SubjectDto>().ReverseMap();
            });

            return mappingConfig;   
        }
    }
}
