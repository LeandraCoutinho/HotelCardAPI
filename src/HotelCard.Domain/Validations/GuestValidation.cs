using FluentValidation;
using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Validations;

public class GuestValidation : AbstractValidator<Guest>
{
    public GuestValidation()
    {
        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(g => g.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.");

        RuleFor(g => g.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(cpf => cpf.ToString().Length == 11).WithMessage("O CPF deve conter 11 dígitos.");

        RuleFor(g => g.CellPhone)
            .NotEmpty().WithMessage("O número de celular é obrigatório.")
            .Matches(@"^\d{10,11}$").WithMessage("O número de celular deve conter 10 ou 11 dígitos.");

        RuleFor(g => g.Address)
            .NotEmpty().WithMessage("O endereço é obrigatório.")
            .MaximumLength(200).WithMessage("O endereço deve ter no máximo 200 caracteres.");

        RuleFor(g => g.DateOfBirth)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.");

        RuleFor(g => g.GuestAccessAreas)
            .NotNull().WithMessage("A lista de áreas de acesso não pode ser nula.")
            .Must(a => a.Count > 0).WithMessage("O hóspede deve ter pelo menos uma área de acesso.");
    }
}