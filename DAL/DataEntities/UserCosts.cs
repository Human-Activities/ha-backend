namespace DAL.DataEntities
{
    public class UserCosts
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public int CostId { get; set; }


        public virtual User User { get; set; }

        public virtual Cost Cost { get; set; }
    }
}
