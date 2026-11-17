namespace SkillLinkApp.Models
{
    public class ProviderRequestViewModel
    {
        public int RequestId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ServiceAddress { get; set; }
        public DateTime? PreferredDate { get; set; }
        public TimeSpan? PreferredTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CustomerCity { get; set; }
        public string? CustomerCountry { get; set; }
    }
}