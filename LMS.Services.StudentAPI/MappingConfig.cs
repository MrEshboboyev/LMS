using AutoMapper;
using LMS.Services.StudentAPI.Models;
using LMS.Services.StudentAPI.Models.Dto;

namespace LMS.Services.StudentAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Student, StudentDto>().ReverseMap();
            });

            return mappingConfig;   
        }
    }
}
