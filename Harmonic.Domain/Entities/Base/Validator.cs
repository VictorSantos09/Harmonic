using FluentValidation;
using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Base;

internal class Validator<TEntity, TKey> : AbstractValidator<TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : IConvertible
{
    public Validator(bool validateId)
    {
        if (validateId)
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
