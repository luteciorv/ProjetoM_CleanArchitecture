namespace CleanArchitecture.Application.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message) : base(message) { }

        public BaseException(string message, object dataException) : this(message) =>
            DataException = dataException;

        public object? DataException { get; private set; }
    }
}
