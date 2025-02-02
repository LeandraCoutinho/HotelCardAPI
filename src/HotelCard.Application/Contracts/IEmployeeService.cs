using HotelCard.Application.Dtos.Employee;

namespace HotelCard.Application.Contracts;

public interface IEmployeeService
{
    Task<EmployeeDto?> Create(AddEmployeeDto employeeDto);
}