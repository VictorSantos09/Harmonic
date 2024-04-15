using FluentValidation;
using Harmonic.Domain.Entities.Base;

namespace Harmonic.Domain.Entities.Pais;

internal class PaisValidator : Validator<PaisEntity, int>
{
    public PaisValidator(bool validateId = false) : base(validateId)
    {
        RuleFor(x => x.Nome).NotEmpty().MaximumLength(45);
    }
}
