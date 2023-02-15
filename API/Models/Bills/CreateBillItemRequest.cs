using API.Models.Categories;

namespace API.Models.Bills
{
    public class CreateBillItemRequest
    {
        public string? BillItemGuid { get; set; }
        public string Name { get; set; }
        public double TotalValue { get; set; }
        public BillItemCategory BillItemCategory { get; set; }
        public Author? Author { get; set; }
    }

    public class Author
    {
        public string? AuthorGuid { get; set; }
        public string? Name { get; set; }
    }
}
