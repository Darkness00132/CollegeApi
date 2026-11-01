namespace College.Application.Common.Results
{
    /// <summary>
    /// The outcome of an operation: either success or failure with an <see cref="Error"/>.
    /// Errors are values, not exceptions — callers decide how to respond.
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, Error? error = null)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("A successful result cannot have an error.");
            if (!isSuccess && error == null)
                throw new InvalidOperationException("A failure result must have an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true);
        public static Result Failure(Error error) => new(false, error);

        public TResult Match<TResult>(
            Func<TResult> onSuccess,
            Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess() : onFailure(Error!);
        }
    }

    /// <summary>
    /// The outcome of an operation that produces a value of <typeparamref name="T"/> on success.
    /// </summary>
    public sealed class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool isSuccess, T? value, Error? error = null) : base(isSuccess, error)
        {
            if (isSuccess && value is null)
                throw new InvalidOperationException("A successful result must have a value.");

            Value = value;
        }

        public static Result<T> Success(T value) => new(true, value);
        public static new Result<T> Failure(Error error) => new(false, default, error);

        public TResult Match<TResult>(
            Func<T, TResult> onSuccess,
            Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
        }
    }
}
