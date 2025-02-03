using FluentValidation;
using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Validations;

public class EmployeeValidation : AbstractValidator<Employee>
{
    public EmployeeValidation()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.");

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email informado é inválido.")
            .EmailAddress().WithMessage("O email informado é inválido.");
    }
    
    public class PasswordValidator : AbstractValidator<string>
    { 
        public PasswordValidator()
        {
            RuleFor(s => s)
                .NotEmpty().WithMessage("O campo Senha é obrigatório.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,20}$")
                .WithMessage("A senha deve conter letras, números, símbolos.");
        }
    }
}