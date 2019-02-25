using System;

namespace Results
{
    public class Result
    {
        public bool IsError { get; private set; }
        public bool IsOK { get => !IsError; }
        public string Message { get; private set; }

        protected Result(bool error, string message=null)
        {
            IsError = error;
            Message = message ?? String.Empty;
        }

        public static Result Failure(string message)
        {
            return new Result(error: true, message: message);
        }

        public static Result<T> Failure<T>( string message, T value=default(T) )
        {
            return new Result<T>( value: value, error: true, message: message );
        }

        public static Result Ok()
        {
            return new Result(error: false);
        }

        public static Result<T> Ok<T>( T value )
        {
            return new Result<T>( value, error: false);
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }
        protected internal Result(T value, bool error, string message=null)
            : base(error, message)
        {
            Value = value;
        }
    }
}
