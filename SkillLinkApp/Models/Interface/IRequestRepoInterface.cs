using SkillLinkApp.Models;
namespace SkillLinkApp.Models.Interface
{
    public interface IRequestRepoInterface
    {
        public List<ServiceRequest> GetAllRequests();
        public ServiceRequest GetRequestById(string id);
        public void UpdateRequest(ServiceRequest request, string id);
        public void UpdateRequestStatus(string requestStatus, string id);
        public void DeleteRequest(string id);
        public void AddRequest(ServiceRequest request);
        List<BookingViewModel> GetBookingsByCustomerId(int customerId);
        List<ProviderRequestViewModel> GetPendingRequestsByService(string serviceCategory);


    }
}
