namespace Domain.Models
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public required decimal Amount { get; set; }
        public required DateTime Date { get; set; }


        public TransferStatus Status { get; set; }
        public  int StatusId { get; set; } //podriamos tener un objeto de tipo Status?
        public required string Description { get; set; }

        public  int TypeId { get; set; }
        public TransferType TransferType { get; set; }

        public required Guid SrcAccountId { get; set; }
        public required Guid DestAccountId { get; set; }
        public required string DestAccountAliasOrCBU { get; set; }
        public bool IsDeleted { get; set; }=false;

    }
}
