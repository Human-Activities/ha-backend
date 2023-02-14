namespace DAL.DataEntities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActivityCategory { get; set; }

        public int? Value { get; set; }
    }
}
