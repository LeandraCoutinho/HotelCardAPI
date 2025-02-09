namespace HotelCard.Application.Dtos.GuestFlow;

public class GuestFlowResponseDto
{
    public int Id { get; set; }
    public string GuestName { get; set; } = null!;
    public string AccessAreaName { get; set; } = null!;
    public DateTime AccessTime { get; set; }
}