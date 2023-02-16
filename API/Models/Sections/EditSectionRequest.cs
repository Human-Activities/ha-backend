using API.Models.Tasks;

namespace API.Models.Sections
{
    public class EditSectionRequest
    {
        public string SectionGuid { get; set; }

        public string Name { get; set; }

        public ICollection<GetTaskResult>? Tasks { get; set; }
    }
}
