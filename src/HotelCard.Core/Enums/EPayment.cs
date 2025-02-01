using System.ComponentModel;

namespace HotelCard.Core.Enums;

public enum EPayment
{
    [Description("Pix")]
    Pix = 1,
    [Description("Cartão de Débito")]
    CartaoDeDebito = 2,
    [Description("Cartão de Crédito")]
    CartaoDeCredito = 2,
}