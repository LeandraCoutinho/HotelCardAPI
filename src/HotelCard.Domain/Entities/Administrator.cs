using HotelCard.Core.Enums;

namespace HotelCard.Domain.Entities;

public class Administrator : Employee
{
    public Administrator()
    {
        Role = ERole.Administrator;
    }
}