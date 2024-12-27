using AutoMapper;
using KuaforShop.Application.DTOs.UserDTOs;
using KuaforShop.Core.Entities;
using KuaforShop.Application.DTOs.SaloonDTOs;
using KuaforShop.Application.DTOs.ServiceDTOs;
using KuaforShop.Application.DTOs.EmployeeDTOs;
using KuaforShop.Application.DTOs.AppointmentDTOs;

namespace KuaforShop.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UserDTO>();
            CreateMap<CreateUserDTO, Users>();
            CreateMap<Saloons, SaloonDTO>();
            CreateMap<CreateSaloonDTO, Saloons>();
            CreateMap<KuaforShop.Core.Entities.Services, ServiceDTO>();
            CreateMap<CreateServiceDTO, KuaforShop.Core.Entities.Services>();
            CreateMap<Employees, EmployeeDTO>();
            CreateMap<CreateEmployeeDTO, Employees>();
            CreateMap<Appointments, AppointmentDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.Name} {src.Employee.Surname}"))
                .ForMember(dest => dest.ServicePrice, opt => opt.MapFrom(src => src.Service.Price));
            CreateMap<CreateAppointmentDTO, Appointments>();
        }
    }
}