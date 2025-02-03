using AutoMapper;
using HotelCard.Application.Dtos.Auth;
using HotelCard.Application.Dtos.Employee;
using HotelCard.Domain.Entities;

namespace HotelCard.Application.Configuration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region Employee

        CreateMap<EmployeeDto, Employee>().ReverseMap();
        CreateMap<AddEmployeeDto, Employee>().ReverseMap();
        CreateMap<LoginDto, Employee>();

        #endregion

        #region



        #endregion
    }
}