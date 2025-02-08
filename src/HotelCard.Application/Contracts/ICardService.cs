using HotelCard.Application.Dtos.Card;
using HotelCard.Application.Dtos.Guest;

namespace HotelCard.Application.Contracts;

public interface ICardService
{
    Task<GuestDto?> RegisterCard(RegisterCardDto registerCardDto);
    Task<bool> ResetCard();
}