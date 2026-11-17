using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillLinkApp.Data;
using SkillLinkApp.Models;
using SkillLinkApp.Models.Interface;
namespace SkillLinkApp.Controllers
{
    [Authorize(Roles = "ServiceProvider")]
    public class ServiceProviderController : Controller
    {

        private readonly IWorkerRepoInterface _workerRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRequestRepoInterface _requestRepo;
        private readonly IRequestOfferRepoInterface _offerRepo;
        public ServiceProviderController(
     UserManager<ApplicationUser> userManager,
     IWorkerRepoInterface workerRepo,
     IRequestRepoInterface requestRepo,
     IRequestOfferRepoInterface offerRepo)
        {
            _userManager = userManager;
            _workerRepo = workerRepo;
            _requestRepo = requestRepo;
            _offerRepo = offerRepo;
        }
        public async Task<IActionResult> Home()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }


            Worker w = _workerRepo.GetWorkerById(user.Id);

            return View(w);
        }
        public async Task<IActionResult> Requests()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Index", "Home");

            var worker = _workerRepo.GetWorkerById(user.Id);

            if (worker == null)
                return NotFound();

            var requests = _requestRepo.GetPendingRequestsByService(worker.ServiceCategory);

            return View(requests);
        }
        public IActionResult MyBookings()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GenerateOffer(int requestId)
        {
            var request = _requestRepo.GetRequestById(requestId.ToString());

            if (request == null)
                return NotFound();

            var model = new GenerateOfferViewModel
            {
                RequestId = request.Id,
                ServiceName = request.ServiceName,
                PreferredDate = request.PreferredDate,
                PreferredTime = request.PreferredTime
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateOffer(GenerateOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Index", "Home");

            var worker = _workerRepo.GetWorkerById(user.Id);
            if (worker == null)
                return NotFound();
            var request = _requestRepo.GetRequestById(model.RequestId.ToString());

            if (request == null)
                return NotFound();

            if (request.Status != "Pending")
                return BadRequest("This request is no longer available.");

            if (request.ServiceName != worker.ServiceCategory)
                return Forbid();

            var offer = new RequestOffer
            {
                RequestId = model.RequestId,
                WorkerId = worker.Id,
                PriceOffer = model.PriceOffer,
                ProposedTime = model.ProposedTime,
                EstimatedDuration = model.EstimatedDuration,
                Message = model.Message,
                Status = "Pending"
            };

            _offerRepo.AddOffer(offer);

            return RedirectToAction("Requests");
        }
    }
}
