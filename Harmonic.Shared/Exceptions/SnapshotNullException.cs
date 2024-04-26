namespace Harmonic.Shared.Exceptions;

public class SnapshotNullException<TSnapshot> : Exception
{
    public SnapshotNullException(string? message = $"{nameof(TSnapshot)} can't be null") : base(message)
    {
    }
}
