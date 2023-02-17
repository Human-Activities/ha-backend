using API.Models.Categories;

namespace API.Models.Bills
{
    public class CreateBillItemRequest
    {
        public string? BillItemGuid { get; set; }
        public string Name { get; set; }
        public double TotalValue { get; set; }
        public int CategoryId { get; set; }
        public string UserGuid { get; set; }
    }
}
