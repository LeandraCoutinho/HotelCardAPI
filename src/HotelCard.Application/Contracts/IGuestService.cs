using HotelCard.Application.Dtos.Guest;

namespace HotelCard.Application.Contracts;

public interface IGuestService
{
    Task<GuestDto?> GetGuest(ulong cardOfNumber);
}