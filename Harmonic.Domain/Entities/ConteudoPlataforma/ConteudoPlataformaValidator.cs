using FluentValidation;
using Harmonic.Domain.Entities.Base;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Shared.Constants;

namespace Harmonic.Domain.Entities.ConteudoPlataforma;

internal class ConteudoPlataformaValidator : Validator<ConteudoPlataformaEntity, int>
{
    public ConteudoPlataformaValidator(bool validateId = false) : base(validateId)
    {
        RuleFor(x => x.Conteudo).SetValidator(new ConteudoValidator());

        RuleFor(x => x.URL).NotEmpty()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .MaximumLength(800)
            .WithMessage(CONSTANTS.STRING.MESSAGE_TAMANHO_EXCEDIDO);

        RuleFor(x => x.Plataforma).SetValidator(new PlataformaValidator());
    }
}
