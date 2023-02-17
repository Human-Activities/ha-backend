namespace API.Models.Bills
{
    public class EditBillRequest : CreateBillRequest
    {
        public string BillGuid { get; set; }
    }
}
