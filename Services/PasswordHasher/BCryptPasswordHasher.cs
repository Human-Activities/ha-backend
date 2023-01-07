namespace Services.PasswordHasher
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        private const int saltRounds = 10;

        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(saltRounds);
        }

        public string Hash(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt, true);
        }
    }
}
