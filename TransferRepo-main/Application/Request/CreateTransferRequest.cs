namespace Application.Request
{
    public class CreateTransferRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public Guid SrcAccountId { get; set; }
        //public Guid DestAccountId { get; set; }
        public string DestAccountAliasOrCBU { get; set; }
    }
}
