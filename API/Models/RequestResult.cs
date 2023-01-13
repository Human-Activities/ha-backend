namespace API.Models
{
    public class RequestResult
    {
        public Guid UserId { get; set; }

        public bool Successful { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
