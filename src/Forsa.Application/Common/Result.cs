namespace Forsa.Application.Common;

public sealed class Result<T> where T : notnull
{
    private readonly T? _value;
    private readonly AppError? _error;

    internal Result(T? value, AppError? error)
    {
        _value = value;
        _error = error;
    }

    public bool IsSuccess => _error is null;
    public T Value => IsSuccess && _value is not null ? _value : throw new InvalidOperationException("A failure has no value.");
    public AppError Error => _error ?? throw new InvalidOperationException("A success has no error.");
}

public static class Result
{
    public static Result<T> Success<T>(T value) where T : notnull => new(value ?? throw new ArgumentNullException(nameof(value)), null);
    public static Result<T> Failure<T>(AppError error) where T : notnull => new(default, error);
}
