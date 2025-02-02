using AutoMapper;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Employee;
using Microsoft.AspNetCore.Identity;
using HotelCard.Application.Notification;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;

namespace HotelCard.Application.Services;

public class EmployeeService : BaseService, IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordHasher<Employee> _hasher;
    private readonly  IEmailService _emailService;

    public EmployeeService(INotificator notificator, IMapper mapper, IEmployeeRepository employeeRepository, IPasswordHasher<Employee> hasher, IEmailService emailService) : base(notificator, mapper)
    {
        _employeeRepository = employeeRepository;
        _hasher = hasher;
        _emailService = emailService;
    }

    public async Task<EmployeeDto?> Create(AddEmployeeDto employeeDto)
    {
        var employeeExists = await _employeeRepository.Get(employeeDto.Email);

        if (employeeExists is not null)
        {
            Notificator.Handle("Email já cadastrado no sistema.");
            return null;
        }

        var employee = Mapper.Map<Employee>(employeeDto);
        Console.WriteLine($"Nome do funcionário: {employee.Name}");
        
        Notificator.Handle(employee.Validate());
        if(Notificator.HasNotification)
            return null;

        var password = Guid.NewGuid().ToString();
        employee.Password = _hasher.HashPassword(employee, password);
        employee.PasswordTemple = true;
        await _employeeRepository.Add(employee);
        
        if (await _employeeRepository.UnitOfWork.Commit())
        {
            await _emailService.SendEmailFirstAccess(employee, password);
            return Mapper.Map<EmployeeDto>(employee);
        }
        
        Notificator.Handle("Não foi possivel criar a entidade");
        return null;
    }
}