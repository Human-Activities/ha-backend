namespace API.Errors
{
    public class OperationError
    {
        public string? ErrorCode { get; set; }
        public string? ErrorDescription { get; set; }

        public OperationError(string? errorCode = null, string? errorDescription = null)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
