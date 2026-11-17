using SkillLinkApp.Models;
namespace SkillLinkApp.Models
{
    public class ServiceRequest
    {

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ServiceId { get; set; }

        public string Description { get; set; }

        public string ServiceAddress { get; set; }
        public DateTime? PreferredDate { get; set; }
        public TimeSpan? PreferredTime { get; set; }
        public string? ServiceName { get; set; }
        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? ConfirmedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Navigation properties
        public Customer? Customer { get; set; }
        public Service? Service { get; set; }

        public ICollection<RequestOffers> Offers { get; set; }
            = new List<RequestOffers>();

    }
}
