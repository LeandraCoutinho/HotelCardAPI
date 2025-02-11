using HotelCard.Application.Dtos.Guest;

namespace HotelCard.Application.Contracts;

public interface IGuestService
{
    Task<GuestDto?> UpdateGuest(int id, UpdateGuestDto updateGuestDto);
    Task<GuestDto?> GetGuestByCard(ulong cardOfNumber);
    Task<GuestResponseDto?> GetGuestByEmail(string email);
    Task<List<GuestDto>> GetAll();
    Task<GuestDto?> DisableGuest(ulong cardOfNumber);
}