using DAL.DataEntities;

namespace API.Models.Activities
{
    public class GetActivityResult : EditActivityRequest
    {
        public IEnumerable<Category> Categories { get; set; } // TODO: change to CategoryVM
    }
}
