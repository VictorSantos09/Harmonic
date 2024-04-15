using FluentValidation;
using Harmonic.Domain.Entities.Base;
using Harmonic.Shared.Constants;

namespace Harmonic.Domain.Entities.Plataforma;

internal class PlataformaValidator : Validator<PlataformaEntity, int>
{
    public PlataformaValidator(bool validateId = false) : base(validateId)
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .MaximumLength(CONSTANTS.INT.DEFAULT_MAX_VALUE)
            .WithMessage(CONSTANTS.STRING.MESSAGE_TAMANHO_EXCEDIDO);

        RuleFor(x => x.URL)
            .NotEmpty()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .MaximumLength(800)
            .WithMessage(CONSTANTS.STRING.MESSAGE_TAMANHO_EXCEDIDO);
    }
}
