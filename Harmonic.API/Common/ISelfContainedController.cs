using QuickKit.AspNetCore.Controllers.Contracts;
using QuickKit.Shared.Entities;

namespace Harmonic.API.Common;

public interface ISelfContainedController<TDTO, TEntity, TKey> : IAddController<TDTO>
    , IUpdateController<TDTO>
    , IDeleteController<TKey>
    , IGetByIdController<TEntity, TKey>
    , IGetAllController<TEntity>
    where TEntity : IEntity
    where TKey : IConvertible
    where TDTO : class
{
}
