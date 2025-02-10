using HotelCard.Application.Dtos.GuestFlow;

namespace HotelCard.Application.Contracts;

public interface IGuestFlowService
{
    Task<GuestFlowResponseDto?> AddFlow(GuestFlowDto dto);
    Task<List<GuestFlowResponseDto>> GetFlows();
    Task<List<GuestFlowResponseDto>> GetFlowsByCpf(ulong cpf);
}