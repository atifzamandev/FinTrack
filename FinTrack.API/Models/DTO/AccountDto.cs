using FinTrack.API.Models.Domains;

namespace FinTrack.API.Models.DTO
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "SEK";
    }
}
