using AutoMapper;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Card;
using HotelCard.Application.Dtos.Guest;
using HotelCard.Application.Notification;
using HotelCard.Domain.Contracts.Repositories;

namespace HotelCard.Application.Services;

public class CardService : BaseService, ICardService
{
    private readonly IGuestRepository _guestRepository;


    public CardService(INotificator notificator, IMapper mapper, IGuestRepository guestRepository) : base(notificator, mapper)
    {
        _guestRepository = guestRepository;
    }

    public async Task<GuestDto?> RegisterCard(RegisterCardDto registerCardDto)
    {
        var guest = await _guestRepository.GetByCpf(registerCardDto.Cpf);
        
        if (guest is null)
        {
            Notificator.Handle("CPF não encontrado no sistema.");
            return null;
        }

        guest.CardOfNumber = registerCardDto.CardOfNumber;
        var guestUpdated =_guestRepository.Update(guest);
            
        if (await CommitChanges())
        {
            return Mapper.Map<GuestDto>(guestUpdated);
        }
        
        Notificator.Handle("Não foi possível atualizar o número do cartão.");
        return null;
    }

    public async Task<bool> ResetCard()
    {
        var inactiveGuests = await _guestRepository.GetInactive();
        
        if (!inactiveGuests.Any()) return false; 

        foreach (var guest in inactiveGuests)
        {
            guest.CardOfNumber = null; 
        }

        await _guestRepository.UpdateRange(inactiveGuests);
        await CommitChanges();
        return true;
    }

    async Task<bool> CommitChanges() => await _guestRepository.UnitOfWork.Commit();
}