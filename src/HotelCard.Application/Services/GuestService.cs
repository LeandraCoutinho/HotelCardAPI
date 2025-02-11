using AutoMapper;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Guest;
using HotelCard.Application.Notification;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;

namespace HotelCard.Application.Services;

public class GuestService : BaseService, IGuestService
{
    private readonly IGuestRepository _guestRepository;
    
    public GuestService(INotificator notificator, IMapper mapper, IGuestRepository guestRepository) : base(notificator, mapper)
    {
        _guestRepository = guestRepository;
    }
    
    public async Task<GuestDto?> UpdateGuest(int id, UpdateGuestDto updateGuestDto)
    {
        if (id != updateGuestDto.Id)
        {   
            Notificator.Handle("Os ids não conferem");
            return null;
        }
        
        var guest = await _guestRepository.GetById(updateGuestDto.Id, true); 

        if (guest is null)
        {
            Notificator.Handle("Hóspede não encontrado.");
            return null;
        }

        guest.Name = updateGuestDto.Name;
        guest.Email = updateGuestDto.Email;
        guest.CellPhone = updateGuestDto.CellPhone;
        guest.Cpf = updateGuestDto.Cpf;
        guest.DateOfBirth = updateGuestDto.DateOfBirth;
        guest.PhotoUrl = updateGuestDto.PhotoUrl;
        
        _guestRepository.RemoveGuestAccessAreas(guest.Id);
        guest.GuestAccessAreas = updateGuestDto.AccessAreaIds
            .Select(areaId => new GuestAccessArea { AccessAreaId =  areaId})
            .ToList();
        
        
        Notificator.Handle(guest.Validate());
        if(Notificator.HasNotification)
            return null;
        
        await _guestRepository.Update(guest);
        if (await CommitChanges())
        {
            return Mapper.Map<GuestDto>(guest);
        }
        
        Notificator.Handle("Não foi possivel atualizar a entidade.");
        return null;
    }

    public async Task<GuestDto?> GetGuestByCard(ulong cardOfNumber)
    {
        var guest = await _guestRepository.GetByCardOfNumber(cardOfNumber);
        
        if (guest is null)
        {
            Notificator.Handle("Hóspede não encontrado na base de dados.");
            return null;
        }

        return Mapper.Map<GuestDto>(guest);
    }

    public async Task<GuestResponseDto?> GetGuestByEmail(string email)
    {
        var guest = await _guestRepository.GetByEmail(email);
        
        if (guest is null)
        {
            Notificator.Handle("Hóspede não encontrado na base de dados.");
            return null;
        }

        return Mapper.Map<GuestResponseDto>(guest);
    }

    public async Task<List<GuestDto>> GetAll()
    {
        var guests = await _guestRepository.GetAll();

        var guestDtos = Mapper.Map<List<GuestDto>>(guests);

        return guestDtos;    
    }

    public async Task<GuestDto?> DisableGuest(ulong cardOfNumber)
    {
        var guest = await _guestRepository.GetByCardOfNumber(cardOfNumber);
        
        if (guest is null)
        {
            Notificator.Handle("Hóspede não encontrado na base de dados.");
            return null;
        }

        guest.IsActive = false;
        
        await _guestRepository.Update(guest);
        if (await CommitChanges())
        {
            return Mapper.Map<GuestDto>(guest);
        }
        
        Notificator.Handle("Não foi possivel desativar a entidade.");
        return null;
    }

    async Task<bool> CommitChanges() => await _guestRepository.UnitOfWork.Commit();
}