using HotelCard.Application.Dtos.Consumption;

namespace HotelCard.Application.Contracts;

public interface IConsumptionService
{
    Task<ConsumptionDto?> Add(AddConsumptionDto consumptionDto);
    Task<List<ConsumptionDto>?> GetConsumptionByGuest(ulong cpf);
    Task<List<ConsumptionDto>> GetAll();
}