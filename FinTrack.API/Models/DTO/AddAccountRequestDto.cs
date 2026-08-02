namespace FinTrack.API.Models.DTO
{
    public class AddAccountRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
