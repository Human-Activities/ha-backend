namespace DAL.DataEntities
{
    public class BillItem
    {
        public int Id { get; set; }

        public Guid BillItemGuid { get; set; }

        public int BillId { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public double TotalValue { get; set; }

        public DateTime CreatedDate { get; set; }


        public virtual Bill Bill { get; set; }

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }
    }
}
