namespace Domain.Models
{
    public class AccountModel
    {
        public Guid AccountId { get; set; }
        public required string CBU { get; set; }
        public required string Alias { get; set; }
        public required string NumberAccount { get; set; }
        public required decimal Balance { get; set; }
        public required int UserId { get; set; }
        public required int AccTypeId { get; set; }
        public AccountType AccountType { get; set; }
        public required int CurrencyId { get; set; }
        public TypeCurrency TypeCurrency { get; set; }
        public required int StateId { get; set; }
        public StateAccount StateAccount { get; set; }
    }
}
