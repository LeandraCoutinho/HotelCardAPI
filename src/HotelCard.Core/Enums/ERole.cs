using System.ComponentModel;

namespace HotelCard.Core.Enums;

public enum ERole
{
    [Description("Administrator")]
    Administrator = 1,
    [Description("Operator")]
    Operator = 2,
}