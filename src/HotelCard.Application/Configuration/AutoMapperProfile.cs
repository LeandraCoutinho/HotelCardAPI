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
        CreateMap<AddHolderDto, Guest>()
            .ForMember(dest => dest.GuestAccessAreas, opt => opt.MapFrom(src =>
                src.AccessAreaIds.Select(id => new GuestAccessArea { AccessAreaId = id })
            ))
            .ReverseMap()
            .ForMember(dest => dest.AccessAreaIds, opt => opt.MapFrom(src =>
                src.GuestAccessAreas.Select(ga => ga.AccessAreaId).ToList()
            ));
        CreateMap<Guest, AddDependentDto>()
            .ForMember(dest => dest.AccessAreaIds, opt => opt.MapFrom(src => src.GuestAccessAreas
                .Select(ga => ga.AccessAreaId).ToList()));

        #endregion
        
        #region Guest

        CreateMap<Guest, UpdateGuestDto>()
            .ForMember(dest => dest.AccessAreaIds, opt => opt.MapFrom(src => src.GuestAccessAreas
                .Select(ga => ga.AccessArea) 
                .ToList()));

        CreateMap<Guest, GuestDto>()
            .ForMember(dest => dest.AccessAreas, opt => opt.MapFrom(src => src.GuestAccessAreas
                .Select(gaa => gaa.AccessArea)
                .ToList())).ReverseMap();

        CreateMap<AccessAreasDto, AccessArea>().ReverseMap();

        #endregion

    }
}