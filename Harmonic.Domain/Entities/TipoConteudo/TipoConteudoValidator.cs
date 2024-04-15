using FluentValidation;
using Harmonic.Domain.Entities.Base;
using Harmonic.Shared.Constants;

namespace Harmonic.Domain.Entities.TipoConteudo;

internal class TipoConteudoValidator : Validator<TipoConteudoEntity, int>
{
    public TipoConteudoValidator(bool validateId = false) : base(validateId)
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .MaximumLength(CONSTANTS.INT.DEFAULT_MAX_VALUE);
    }
}
