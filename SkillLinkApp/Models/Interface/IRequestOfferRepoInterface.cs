using SkillLinkApp.Models;
namespace SkillLinkApp.Models.Interface
{
    public interface IRequestOfferRepoInterface
    {
        void AddOffer(RequestOffer offer);
        List<CustomerOfferViewModel> GetOffersByRequestId(int requestId);
    }
}
