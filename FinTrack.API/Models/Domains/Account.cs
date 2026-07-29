namespace FinTrack.API.Models.Domains
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "SEK";

       public ICollection<Transaction> Transactions { get; set; } =  new List<Transaction>();

    }
}
