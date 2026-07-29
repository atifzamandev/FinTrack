namespace FinTrack.API.Models.Domains
{
    public class TransactionCategory
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    = new List<Transaction>();

    }
}
                