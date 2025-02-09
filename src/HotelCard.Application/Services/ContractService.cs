using AutoMapper;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Contract;
using HotelCard.Application.Notification;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;

namespace HotelCard.Application.Services;

public class ContractService : BaseService, IContractService
{
    private readonly IContractRepository _contractRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IAccessAreaRepository _accessAreaRepository;

    public ContractService(INotificator notificator, IMapper mapper, IContractRepository contractRepository, IRoomRepository roomRepository, IAccessAreaRepository accessAreaRepository) : base(notificator, mapper)
    {
        _contractRepository = contractRepository;
        _roomRepository = roomRepository;
        _accessAreaRepository = accessAreaRepository;
    }

    public async Task<ContractDto?> Create(ContractDto contractDto)
    {
        var isHolderRoomAvailable = await _roomRepository.IsCheckAvailability(contractDto.Holder.Room);

        if (!isHolderRoomAvailable)
        {
            Notificator.Handle("Quarto do titular não está disponível.");
            return null;
        }

        foreach (var dependent in contractDto.Dependents)
        {
            var isDependentRoomAvailable = await _roomRepository.IsCheckAvailability(dependent.Room);
        
            if (!isDependentRoomAvailable)
            {
                Notificator.Handle("Quarto do dependente não está disponível.");
                return null;
            }
        }
        
        var holderGuest = new Guest
        {
            Name = contractDto.Holder.Name,
            Email = contractDto.Holder.Email,
            Cpf = contractDto.Holder.Cpf,
            CellPhone = contractDto.Holder.CellPhone,
            Address = contractDto.Holder.Address,
            DateOfBirth = contractDto.Holder.DateOfBirth,
            PhotoUrl = contractDto.Holder.PhotoUrl,
            IsHolder = true,
            CreatedAt = DateTime.Now,
            GuestAccessAreas = contractDto.Holder.AccessAreaIds
                .Select(accessAreaId => new GuestAccessArea
                {
                    AccessAreaId = accessAreaId
                })
                .ToList()
        };

        var dependentGuests = contractDto.Dependents.Select(dependent => new Guest
        {
            Name = dependent.Name,
            Email = dependent.Email,
            IsHolder = false,
            Cpf = dependent.Cpf,
            DateOfBirth = dependent.DateOfBirth,
            PhotoUrl = dependent.PhotoUrl,
            CreatedAt = DateTime.Now,
            GuestAccessAreas = dependent.AccessAreaIds
                .Select(accessAreaId => new GuestAccessArea
                {
                    AccessAreaId = accessAreaId
                })
                .ToList()
        }).ToList();

        var contract = new Contract
        {
            BeginAt = contractDto.BeginAt,
            FinishAt = contractDto.FinishAt,
            PaymentId = contractDto.PaymentId,
            Holder = holderGuest,
            HolderId = holderGuest.Id, 
            Dependents = dependentGuests,
            Rooms = new List<Room>(), 
            ContractRooms = new List<ContractRoom>() 
        };
        
        contract.HolderId = holderGuest.Id;
        
        contract.ContractRooms.Add(new ContractRoom
        {
            Contract = contract,
            RoomId = contractDto.Holder.Room
        });

        contract.ContractRooms.AddRange(contractDto.Dependents.Select(dependent => new ContractRoom
        {
            Contract = contract,
            RoomId = dependent.Room
        }));
        
        contract.Holder = holderGuest;
        contract.HolderId = holderGuest.Id;

        var contractCreated = await _contractRepository.Add(contract);
        
        await ReserveRoom(contractDto.Holder.Room); 
        foreach (var dependent in contractDto.Dependents)
        {
            await ReserveRoom(dependent.Room); 
        }
        
        if (await CommitChanges())
        {
            var contractWithDetails = await _contractRepository.GetById(contractCreated.Id, true);

            return Mapper.Map<ContractDto>(contractWithDetails);
        }
        
        Notificator.Handle("Não foi possivel criar a entidade.");
        return null;
    }
    
    private async Task ReserveRoom(int roomId)
    {
        var room = await _roomRepository.GetById(roomId); 
        if (room != null)
        {
            room.IsReserved = true; 
            await _roomRepository.Update(room); 
        }
    }
    
    async Task<bool> CommitChanges() => await _contractRepository.UnitOfWork.Commit();
}