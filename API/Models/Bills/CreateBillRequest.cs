namespace API.Models.Bills
{
    public class CreateBillRequest
    {
        public string UserGuid { get; set;}
        public string? GroupGuid { get; set;}
        public string Name { get; set;}
        
        public ICollection<CreateBillItemRequest> BillItems { get; set;}
    }
}
