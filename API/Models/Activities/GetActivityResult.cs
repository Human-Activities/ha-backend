using API.Models.Categories;
using DAL.DataEntities;

namespace API.Models.Activities
{
    public class GetActivityResult : EditActivityRequest
    {
        public Author Author { get; set; }
    }
}
