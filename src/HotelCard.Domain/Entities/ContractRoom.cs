namespace HotelCard.Domain.Entities;

public class ContractRoom
{
    public int ContractId { get; set; }
    public Contract Contract { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
}