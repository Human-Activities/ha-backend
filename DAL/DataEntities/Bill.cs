using DAL.CommonVariables;

namespace DAL.DataEntities
{
    public class Bill
    {
        public Bill()
        {
            BillItems = new List<BillItem>();
        }

        public int Id { get; set; }

        public Guid BillGuid { get; set; }

        public int UserId { get; set; }

        public int? GroupId { get; set; }

        public string Name { get; set; }

        public double TotalValue { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AccountBillNumber { get; set; } // wartosc dla uzytkownika, powiekszona o 1 w poronwaniu dla ostatniego najnowszego


        public virtual ICollection<BillItem> BillItems { get; set; }

        public virtual User User { get; set; }

        public virtual Group? Group { get; set; }
    }
}
