namespace Application.Response
{
    public class PersonalAccountResponse
    {
        public Guid Id { get; set; }
        public string CBU { get; set; }
        public string BankName { get; set; }
        public int AccountNumber { get; set; }
        public int AccountType { get; set; }
        public int UserId { get; set; }
    }
}
