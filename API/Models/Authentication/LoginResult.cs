namespace API.Models.Authentication
{
    public class LoginResult
    {
        public Guid UserGuid { get; set; }

        public bool Successful { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
