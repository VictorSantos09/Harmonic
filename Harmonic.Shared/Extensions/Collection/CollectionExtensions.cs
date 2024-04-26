using QuickKit.Shared.Entities;

namespace Harmonic.Shared.Extensions.Collection;

public static class CollectionExtensions
{
    public static IEnumerable<TEntity> ToEntities<TEntity, TSnapshot, TKey>(this IEnumerable<TSnapshot> snapshots)
        where TEntity : IEntity<TEntity, TSnapshot, TKey>
        where TSnapshot : class
        where TKey : IConvertible
    {
        return snapshots.Any() ? snapshots.Select(TEntity.FromSnapshot) : Enumerable.Empty<TEntity>();
    }
}
