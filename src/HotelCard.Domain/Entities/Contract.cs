using HotelCard.Core.Enums;

namespace HotelCard.Domain.Entities;

public class Contract : Entity
{
    public DateTime BeginAt { get; set; }
    public DateTime FinishAt { get; set; }
    public int HolderId { get; set; }
    public EPayment PaymentId { get; set; }
    public Guest Holder { get; set; }
    public IList<Guest> Dependents { get; set; } = new List<Guest>();
    public IList<Room> Rooms { get; set; } = new List<Room>(); // ver isso
    public IList<ContractRoom> ContractRooms { get; set; } = new List<ContractRoom>();
}