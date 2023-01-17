namespace DAL.DataEntities
{
    public class Task
    {
        public int Id { get; set; }

        public Guid TaskGuid { get; set; }

        public string Name { get; set; }

        public string? Notes { get; set; }

        public bool IsDone { get; set; }

        public int SectionId { get; set; }


        public virtual Section Section { get; set; }
    }
}
