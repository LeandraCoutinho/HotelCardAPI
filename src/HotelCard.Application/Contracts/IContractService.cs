using HotelCard.Application.Dtos.Contract;

namespace HotelCard.Application.Contracts;

public interface IContractService
{
    Task<ContractDto> Create(ContractDto contractDto);
}