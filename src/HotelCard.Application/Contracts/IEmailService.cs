using HotelCard.Domain.Entities;

namespace HotelCard.Application.Contracts;

public interface IEmailService
{
    Task SendPasswordRecovery(Employee employee);
}