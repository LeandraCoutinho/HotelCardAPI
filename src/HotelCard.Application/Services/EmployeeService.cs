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

    public EmployeeService(INotificator notificator, IMapper mapper, IEmployeeRepository employeeRepository, IPasswordHasher<Employee> hasher) : base(notificator, mapper)
    {
        _employeeRepository = employeeRepository;
        _hasher = hasher;
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
        
        Notificator.Handle(employee.Validate());
        if(Notificator.HasNotification)
            return null;

        employee.Password = _hasher.HashPassword(employee, employee.Password);
        var employeeCreated = await _employeeRepository.Add(employee);
        
        if (await CommitChanges())
        {
            return Mapper.Map<EmployeeDto>(employeeCreated);
        }
        
        Notificator.Handle("Não foi possivel criar a entidade");
        return null;
    }
    
    async Task<bool> CommitChanges() => await _employeeRepository.UnitOfWork.Commit();
}