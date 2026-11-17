namespace SkillLinkApp.Models
{
    public class RequestOffer
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int WorkerId { get; set; }

        public decimal PriceOffer { get; set; }
        public TimeSpan? ProposedTime { get; set; }
        public int EstimatedDuration { get; set; }

        public string? Message { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
    }
}