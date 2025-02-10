using AutoMapper;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Consumption;
using HotelCard.Application.Notification;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;

namespace HotelCard.Application.Services;

public class ConsumptionService : BaseService, IConsumptionService
{
    private readonly IConsumptionRepository _consumptionRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IProductRepository _productRepository;

    public ConsumptionService(INotificator notificator, IMapper mapper, IConsumptionRepository consumptionRepository, IGuestRepository guestRepository, IProductRepository productRepository) : base(notificator, mapper)
    {
        _consumptionRepository = consumptionRepository;
        _guestRepository = guestRepository;
        _productRepository = productRepository;
    }
    
    public async Task<ConsumptionDto?> Add(AddConsumptionDto dto)
    {
        var guest = await _guestRepository.GetByCardOfNumber(dto.CardNumber);
        if (guest is null)
        {
            Notificator.Handle("Hóspede não encontrado.");
            return null;
        }

        var consumption = new Consumption
        {
            DateConsumption = DateTime.UtcNow,
            PaymentId = dto.Payment,
            GuestId = guest.Id,
            ConsumptionProducts = new List<ConsumptionProduct>()
        };

        foreach (var productDto in dto.Products)
        {
            var existingProduct = await _productRepository.GetById(productDto.ProductId);
            if (existingProduct is null)
            {
                Notificator.Handle($"Produto com ID {productDto.ProductId} não encontrado.");
                return null;
            }

            double totalValue = existingProduct.Price * productDto.Quantity;
            
            consumption.ConsumptionProducts.Add(new ConsumptionProduct
            {
                ProductId = productDto.ProductId,
                Quantity = productDto.Quantity,
                TotalValue = totalValue
            });
        }
        
        var consumptionCreated = await _consumptionRepository.Add(consumption);

        if (await CommitChanges())
        {
            var consumptionDto = new ConsumptionDto
            {
                Id = consumptionCreated.Id,
                GuestName = guest.Name, 
                DateConsumption = consumptionCreated.DateConsumption,
                Products = consumptionCreated.ConsumptionProducts
                    .Select(cp => new ProductDto
                    {
                        ProductId = cp.ProductId,
                        Price = _productRepository.GetById(cp.ProductId).Result?.Price ?? 0, 
                        Quantity = cp.Quantity,
                        TotalValue = cp.TotalValue
                    })
                    .ToList()
            };

            return consumptionDto;
        }

        Notificator.Handle("Não foi possível atualizar a entidade.");
        return null;
    }

    public async Task<List<ConsumptionDto>?> GetConsumptionByGuest(ulong cpf)
    {
        var guest = await _guestRepository.GetByCpf(cpf);
        if (guest is null)
        {
            Notificator.Handle("Hóspede não encontrado.");
            return null;
        }

        var consumptions = await _consumptionRepository.GetByGuestId(guest.Id);
        if (!consumptions.Any())
        {
            Notificator.Handle("Nenhuma consumação encontrada para este hóspede.");
            return null;
        }

        var consumptionDtos = new List<ConsumptionDto>();

        foreach (var consumption in consumptions)
        {
            var consumptionDto = new ConsumptionDto
            {
                Id = consumption.Id,
                GuestName = guest.Name, 
                DateConsumption = consumption.DateConsumption,
                Products = consumption.ConsumptionProducts
                    .Select(cp => new ProductDto
                    {
                        ProductId = cp.ProductId,
                        Price = _productRepository.GetById(cp.ProductId).Result?.Price ?? 0, 
                        Quantity = cp.Quantity,
                        TotalValue = cp.TotalValue
                    })
                    .ToList()
            };

            consumptionDtos.Add(consumptionDto);
        }

        return consumptionDtos;
    }

    public async Task<List<ConsumptionDto>> GetAll()
    {
        var consumptions = await _consumptionRepository.GetAll();

        var consumptionDto =  Mapper.Map<List<ConsumptionDto>>(consumptions);

        return consumptionDto;
    }
    
    async Task<bool> CommitChanges() => await _consumptionRepository.UnitOfWork.Commit();
}