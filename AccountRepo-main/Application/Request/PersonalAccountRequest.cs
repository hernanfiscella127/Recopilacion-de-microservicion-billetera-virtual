namespace Application.Request
{
    public class PersonalAccountRequest
    {
        public required string CBU { get; set; }
        public required string BankName { get; set; }
        public required int AccountNumber { get; set; }
        public required int AccountType { get; set; }
        public required int UserId { get; set; }
    }
}
