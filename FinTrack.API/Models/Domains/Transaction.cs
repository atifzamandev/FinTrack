using FinTrack.API.Models.Enums;

namespace FinTrack.API.Models.Domains
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }

        public Guid AccountId { get; set; }
        public Guid TransactionCategoryId { get; set; }

        // initialized with null-forgiving because EF will populate them
        public required Account Account { get; set; } 
        public required TransactionCategory TransactionCategory { get; set; } 
    }
}
