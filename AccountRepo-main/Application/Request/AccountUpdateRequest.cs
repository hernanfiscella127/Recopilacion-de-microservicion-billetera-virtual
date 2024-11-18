namespace Application.Request
{
    public class AccountUpdateRequest
    {
        public string? Alias { get; set; } 
        public int? Currency { get; set; }
        public int? State { get; set; }
        public int? AccountType { get; set; }
    }
}
