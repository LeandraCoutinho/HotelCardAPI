using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IGuestFlowRepository
{
    Task<GuestFlow> Add(GuestFlow guestFlow);
    Task<List<GuestFlow>> GetAll();
    IUnitOfWork UnitOfWork { get; }
    Task<List<GuestFlow>> GetByCpf(ulong cpf);
}