using AutoMapper;
using HotelCard.Application.Dtos.Auth;
using HotelCard.Application.Dtos.Card;
using HotelCard.Application.Dtos.Contract;
using HotelCard.Application.Dtos.Employee;
using HotelCard.Application.Dtos.Guest;
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

        #region Contract

        CreateMap<ContractDto, Contract>().ReverseMap();
        CreateMap<RegisterCardDto, Guest>().ReverseMap();
        CreateMap<AddHolderDto, Guest>().ReverseMap();
        CreateMap<Guest, AddDependentDto>()
            .ForMember(dest => dest.AccessAreaIds, opt => opt.MapFrom(src => src.GuestAccessAreas
                .Select(ga => ga.AccessAreaId).ToList()));

        #endregion
    }
}