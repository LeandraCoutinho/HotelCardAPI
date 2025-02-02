using FluentValidation.Results;
using HotelCard.Core.Enums;
using HotelCard.Domain.Validations;

namespace HotelCard.Domain.Entities;

public class Employee : Entity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool PasswordTemple { get; set; }
    public string? TokenResetPassword { get; set; }
    public DateTime? TokenResetExpiresAt { get; set; }
    public ERole Role { get; set; }
    
    public List<ValidationFailure> Validate()
    {
        List<string> Erros = new List<string>();
        var validateHandler = new EmployeeValidation();

        var response = validateHandler.Validate(this);

        return response.Errors;
    }
}