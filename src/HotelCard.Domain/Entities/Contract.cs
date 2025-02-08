using HotelCard.Core.Enums;

namespace HotelCard.Domain.Entities;

public class Contract : Entity
{
    public DateTime BeginAt { get; set; }
    public DateTime FinishAt { get; set; }
    public int HolderId { get; set; } // ver isso
    public EPayment PaymentId { get; set; }
    public Guest Holder { get; set; }
    public List<Guest> Dependents { get; set; } = new List<Guest>();
    public IList<Room> Rooms { get; set; } = new List<Room>(); 
    public List<ContractRoom> ContractRooms { get; set; } = new List<ContractRoom>();
}