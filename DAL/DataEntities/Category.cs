namespace DAL.DataEntities
{
    public class Category
    {
        public int Id { get; set; }

        public Guid CategoryGuid { get; set; }

        public string Name { get; set; }

        public int ActivityId { get; set; }


        public virtual Activity Activity { get; set; }
    }
}
