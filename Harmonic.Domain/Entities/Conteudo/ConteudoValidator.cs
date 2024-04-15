using FluentValidation;
using Harmonic.Domain.Entities.Base;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Shared.Constants;

namespace Harmonic.Domain.Entities.Conteudo;

internal class ConteudoValidator : Validator<ConteudoEntity, int>
{
    public ConteudoValidator(bool validateId = false) : base(validateId)
    {
        RuleFor(x => x.DataCadastro).NotEmpty().WithMessage(CONSTANTS.DATE.INVALIDA);

        RuleFor(x => x.Titulo).NotEmpty().MaximumLength(CONSTANTS.INT.DEFAULT_MAX_VALUE);

        RuleFor(x => x.Descricao).NotNull()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .MaximumLength(800);

        RuleFor(x => x.Pais).SetValidator(new PaisValidator(validateId));
        RuleFor(x => x.TipoConteudo).SetValidator(new TipoConteudoValidator(validateId));
        RuleFor(x => x.Feedback).SetValidator(new FeedbackValidator(validateId));
    }
}
