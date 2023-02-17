namespace API.Models.Bills
{
    public class CreateBillRequest
    {
        public string? BillGuid {get; set;}
        public string UserGuid { get; set;}
        public string? GroupGuid { get; set;}
        public string Name { get; set;}
        public double? TotalValue { get; set;}
        public DateTime? CreatedDate { get; set;}
        public int? AccountBillNumber { get; set;}
        
        public ICollection<CreateBillItemRequest> BillItems { get; set;}
    }
}
