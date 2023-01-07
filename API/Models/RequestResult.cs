namespace API.Models
{
    public class RequestResult
    {
        public bool Successful { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
