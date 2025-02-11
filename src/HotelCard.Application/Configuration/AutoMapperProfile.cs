using AutoMapper;
using HotelCard.Application.Dtos.Auth;
using HotelCard.Application.Dtos.Card;
using HotelCard.Application.Dtos.Consumption;
using HotelCard.Application.Dtos.Contract;
using HotelCard.Application.Dtos.Employee;
using HotelCard.Application.Dtos.Guest;
using HotelCard.Application.Dtos.GuestFlow;
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
        CreateMap<GuestResponseDto, Guest>().ReverseMap();
        
        #endregion
        
        #region GuestFlow
        
        CreateMap<GuestFlow, GuestFlowResponseDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => src.Guest.Name))
            .ForMember(dest => dest.AccessAreaName, opt => opt.MapFrom(src => src.AccessArea.Name));
        CreateMap<GuestFlow, GuestFlowDto>().ReverseMap();

        #endregion
        
        #region Consumption
        
        CreateMap<AddConsumptionDto, Consumption>()
            .ForMember(dest => dest.GuestId, opt => opt.Ignore()) 
            .ForMember(dest => dest.DateConsumption, opt => opt.Ignore()) 
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment))
            .ForMember(dest => dest.ConsumptionProducts, opt => opt.MapFrom(src => src.Products));

        CreateMap<ProductConsumptionDto, ConsumptionProduct>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<Consumption, ConsumptionDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => src.Guest.Name)) 
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ConsumptionProducts));

        CreateMap<ConsumptionProduct, ProductDto>().ReverseMap();

        #endregion

    }
}