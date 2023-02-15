using API.Models.Activities;

namespace API.Models.Bills
{
    public class DeleteBillResult : CreateActivityResult
    {
        public DeleteBillResult(string message) : base(message)
        {
        }
    }
}
