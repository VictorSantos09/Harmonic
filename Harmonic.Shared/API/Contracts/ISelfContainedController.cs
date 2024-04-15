using QuickKit.AspNetCore.Controllers.Contracts;
using QuickKit.Shared.Entities;

namespace Harmonic.Shared.API.Contracts;

public interface ISelfContainedController<TEntity, TDTO, TKey>
    : IAddController<TDTO>
    , IUpdateController<TDTO>
    , IDeleteController<TKey>
    , IGetAllController<TEntity>
    , IGetByIdController<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IConvertible
{
}
