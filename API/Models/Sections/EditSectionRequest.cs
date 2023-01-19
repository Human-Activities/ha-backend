using API.Models.Tasks;

namespace API.Models.Sections
{
    public class EditSectionRequest
    {
        public string SectionGuid { get; set; }

        public string Name { get; set; }

        public IEnumerable<EditTaskRequest>? Tasks { get; set; }
    }
}
