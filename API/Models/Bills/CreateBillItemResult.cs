using API.Models.Categories;

namespace API.Models.Bills
{
    public class CreateBillItemResult : CreateBillItemRequest
    {
        public BillItemCategory? BillItemCategory { get; set; }
        public Author? Author { get; set; }
    }
}
