
namespace SkillLinkApp.Models
{
    public class GenerateOfferViewModel
    {
        public int RequestId { get; set; }

        public string? ServiceName { get; set; }

        public DateTime? PreferredDate { get; set; }

        public TimeSpan? PreferredTime { get; set; }

        public decimal PriceOffer { get; set; }

        public TimeSpan? ProposedTime { get; set; }

        public int EstimatedDuration { get; set; }

        public string? Message { get; set; }
    }
}
