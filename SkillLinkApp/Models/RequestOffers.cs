namespace SkillLinkApp.Models
{
    public class RequestOffers
    {

        public int Id { get; set; }

        public int RequestId { get; set; }

        public int WorkerId { get; set; }

        public decimal PriceOffer { get; set; }

        public string? Message { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ServiceRequest ServiceRequest { get; set; }

        public Worker Worker { get; set; }
    }
}
