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
}