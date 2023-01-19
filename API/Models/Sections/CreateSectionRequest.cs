using API.Models.Tasks;

namespace API.Models.Sections
{
    public class CreateSectionRequest
    {
        public string Name { get; set; }

        public ICollection<CreateTaskRequest>? Tasks { get; set; }
    }
}
