using DAL.CommonVariables;

namespace DAL.DataEntities
{
    public class Cost
    {
        public Cost()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public CostType CostType { get; set; }

        public virtual ICollection<User> Users { get; set; } // I am not sure, if we can do it only with UserId or smth similar
    }
}
