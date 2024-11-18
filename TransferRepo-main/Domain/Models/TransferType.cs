namespace Domain.Models
{
    public class TransferType
    {
        public int TransferTypeId { get; set; }
        public required string Name { get; set; }
        public List<Transfer>Transfers { get; set; }
    }
}
