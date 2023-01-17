namespace API.Models.Authentication
{
    public class LogoutResult
    {
        public LogoutResult(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
