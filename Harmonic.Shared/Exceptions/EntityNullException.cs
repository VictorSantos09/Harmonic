using QuickKit.Shared.Entities;

namespace Harmonic.Shared.Exceptions;

public class EntityNullException : Exception
{
    public EntityNullException(string? message) : base(message)
    {
    }
}

public class EntityNullException<TEntity> : EntityNullException
    where TEntity : IEntity
{
    private static readonly string _defaultMessage = BuildDefaultMessage();
    public EntityNullException() : base(_defaultMessage)
    {
    }
    private protected static string BuildDefaultMessage()
    {
        return $"A entidade {nameof(TEntity)} é nula";
    }
}

public class NullException : Exception
{
    public NullException(string? message) : base(message)
    {
    }
}

public class NullException<TType> : EntityNullException
{
    private static readonly string _defaultMessage = BuildDefaultMessage();
    public NullException() : base(_defaultMessage)
    {
    }
    private protected static string BuildDefaultMessage()
    {
        return $"{nameof(TType)} é nula";
    }
}
