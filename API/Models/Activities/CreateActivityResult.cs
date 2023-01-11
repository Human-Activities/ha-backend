using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Models.Activities
{
    public class CreateActivityResult
    {
        public string Message { get; set; }

        public CreateActivityResult(string message)
        {
            Message = message;
        }
    }
}
