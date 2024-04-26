using FluentValidation;
using Harmonic.Domain.Entities.Base;
using Harmonic.Shared.Constants;

namespace Harmonic.Domain.Entities.Pais;

internal class PaisValidator : Validator<PaisEntity, int>
{
    public PaisValidator(bool validateId = false) : base(validateId)
    {
        RuleFor(x => x.Nome).NotEmpty()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .MaximumLength(CONSTANTS.INT.DEFAULT_MAX_VALUE)
            .WithMessage(CONSTANTS.INT.MESSAGE_VALOR_MAXIMO_FORNECIDO);
    }
}
