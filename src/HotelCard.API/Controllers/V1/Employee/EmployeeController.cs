using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Employee;
using HotelCard.Application.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCard.API.Controllers.V1.Employee;

[AllowAnonymous]
[Route("[controller]")]
public class EmployeeController : BaseController
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(INotificator notificator, IEmployeeService employeeService) : base(notificator)
    {
        _employeeService = employeeService;
    }
    
    [HttpPost("add-employee")]
    public async Task<IActionResult> Create([FromQuery]AddEmployeeDto employee)
    {
        return CustomResponse(await _employeeService.Create(employee));
    }
}