using HotelCard.Domain.Entities;

namespace HotelCard.Application.Contracts;

public interface IEmailService
{
    Task SendEmailFirstAccess(Employee employee, string password);
}