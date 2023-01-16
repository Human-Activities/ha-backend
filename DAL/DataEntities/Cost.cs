using DAL.CommonVariables;

namespace DAL.DataEntities
{
    public class Cost
    {
        public Cost()
        {
            UserCosts = new HashSet<UserCosts>();
        }

        public int Id { get; set; }

        public Guid CostGuid { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public CostType CostType { get; set; }


        public virtual ICollection<UserCosts> UserCosts { get; set; } // I am not sure, if we can do it only with UserId or smth similar
    }
}
