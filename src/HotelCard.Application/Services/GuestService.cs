using AutoMapper;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Guest;
using HotelCard.Application.Notification;
using HotelCard.Domain.Contracts.Repositories;

namespace HotelCard.Application.Services;

public class GuestService : BaseService, IGuestService
{
    private readonly IGuestRepository _guestRepository;
    
    public GuestService(INotificator notificator, IMapper mapper, IGuestRepository guestRepository) : base(notificator, mapper)
    {
        _guestRepository = guestRepository;
    }

    public async Task<GuestDto?> GetGuest(ulong cardOfNumber)
    {
        var guest = await _guestRepository.GetByCardOfNumber(cardOfNumber);
        
        if (guest is not null)
        {
            Notificator.Handle("Hóspede não encontrado na base de dados.");
            return null;
        }

        return Mapper.Map<GuestDto>(guest);
    }
    
    async Task<bool> CommitChanges() => await _guestRepository.UnitOfWork.Commit();
}