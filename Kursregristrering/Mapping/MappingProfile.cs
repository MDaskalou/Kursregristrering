using System.Runtime;
using AutoMapper;
using Kursregristrering.Models;
using Kursregristrering.DTOs;

namespace Kursregristrering.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //skapar en mapping mellan Course och CourseDTO
            CreateMap<Course, CourseDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Enrollment, EnrollmentDTO>();
        }
    }
}
