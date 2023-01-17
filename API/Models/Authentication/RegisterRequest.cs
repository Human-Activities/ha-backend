namespace API.Models.Authentication
{
    public class RegisterRequest
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
