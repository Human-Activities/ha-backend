namespace API.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        public string Login { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }
    }
}
