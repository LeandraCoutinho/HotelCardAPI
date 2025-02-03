namespace HotelCard.Core.Settings;

public class JwtSettings
{
    public int ExpirationHours { get; set; }
    public string Emitter { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
}