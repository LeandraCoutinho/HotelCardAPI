using AutoMapper;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.GuestFlow;
using HotelCard.Application.Notification;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;

namespace HotelCard.Application.Services;

public class GuestFlowService : BaseService, IGuestFlowService
{
    private readonly IGuestFlowRepository _guestFlowRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IAccessAreaRepository _accessAreaRepository;

    public GuestFlowService(INotificator notificator, IMapper mapper, IGuestFlowRepository guestFlowRepository, IGuestRepository guestRepository, IAccessAreaRepository accessAreaRepository) : base(notificator, mapper)
    {
        _guestFlowRepository = guestFlowRepository;
        _guestRepository = guestRepository;
        _accessAreaRepository = accessAreaRepository;
    }
    
    public async Task<GuestFlowResponseDto?> AddFlow(GuestFlowDto dto)
    {
        var guest = await _guestRepository.GetByCardOfNumber(dto.CardOfNumber);
        if (guest is null)
        {
            Notificator.Handle("Hóspede não encontrado.");
            return null;
        }

        var accessArea = await _accessAreaRepository.GetById(dto.AccessAreaId);
        if (accessArea is null)
        {
            Notificator.Handle("Área de acesso não encontrada.");
            return null;
        }

        var hasAccess = guest.GuestAccessAreas.Any(gaa => gaa.AccessAreaId == dto.AccessAreaId);
        if (!hasAccess)
        {
            Notificator.Handle("Hóspede não tem permissão para acessar essa área.");
            return null;
        }

        var guestFlow = new GuestFlow
        {
            GuestId = guest.Id,
            AccessAreaId = dto.AccessAreaId,
            AccessTime = DateTime.Now
        };

        await _guestFlowRepository.Add(guestFlow);
        if (await CommitChanges())
        {
            return Mapper.Map<GuestFlowResponseDto>(guestFlow);
        }
        
        Notificator.Handle("Não foi possivel adicionar a entidade.");
        return null;
    }

    public async Task<List<GuestFlowResponseDto>> GetFlows()
    {
        var flows = await _guestFlowRepository.GetAll();
        return Mapper.Map<List<GuestFlowResponseDto>>(flows);
    }
    
    async Task<bool> CommitChanges() => await _guestFlowRepository.UnitOfWork.Commit();
}