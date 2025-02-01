using HotelCard.Core.Enums;

namespace HotelCard.Domain.Entities;

public class Operator : Employee
{
    public Operator()
    {
        Role = ERole.Operator;
    }
}