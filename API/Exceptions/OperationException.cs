using API.Errors;

namespace API.Exceptions
{
    public class OperationException : Exception
    {
        public int StatusCode { get; set; }

        public OperationError? Error { get; set; }

        public OperationException(int statusCode, OperationError? error = null) : base()
        {
            StatusCode = statusCode;
            Error = error;
        }

        public OperationException(int statusCode, string errorCode, string errorDescription = null) : base()
        {
            StatusCode = statusCode;
            Error = new OperationError(errorCode, errorDescription);
        }
    }
}
