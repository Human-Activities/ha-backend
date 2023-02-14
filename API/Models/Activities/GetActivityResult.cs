using DAL.DataEntities;

namespace API.Models.Activities
{
    public class GetActivityResult : EditActivityRequest
    {
        public Category Category { get; set; } // TODO: change to CategoryVM
    }
}
