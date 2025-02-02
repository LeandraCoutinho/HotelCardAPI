using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<Employee?> Get(string email)
    {
        var employee = await Context.Employees.FirstOrDefaultAsync(e => e.Email.Equals(email));

        return employee;
    }
}