using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Contract;
using HotelCard.Application.Notification;
using HotelCard.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCard.API.Controllers.V1.Contract;

[Route("[controller]")]
public class ContractController : BaseController
{
    private readonly IContractService _contractService;
    
    public ContractController(INotificator notificator, IContractService contractService) : base(notificator)
    {
        _contractService = contractService;
    }
    
    [Authorize(Roles = nameof(ERole.Administrator))]
    [HttpPost("create-contract")]
    [ProducesResponseType(typeof(ContractDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] ContractDto contractDto)
    {
        return CustomResponse(await _contractService.Create(contractDto));
    }
}