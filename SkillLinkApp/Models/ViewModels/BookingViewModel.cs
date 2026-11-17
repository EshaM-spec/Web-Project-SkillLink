namespace SkillLinkApp.Models
{
    public class BookingViewModel
    {
        public int RequestId { get; set; }

        public string ServiceName { get; set; }

        public string Description { get; set; }

        public string ServiceAddress { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? WorkerName { get; set; }
    }
}