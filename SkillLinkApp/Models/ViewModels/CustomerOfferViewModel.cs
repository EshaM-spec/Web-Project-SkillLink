namespace SkillLinkApp.Models
{
    public class CustomerOfferViewModel
    {
        public int OfferId { get; set; }
        public int RequestId { get; set; }

        public string WorkerName { get; set; }

        public decimal PriceOffer { get; set; }
        public TimeSpan? ProposedTime { get; set; }
        public int EstimatedDuration { get; set; }

        public string? Message { get; set; }
        public string Status { get; set; }
    }
}