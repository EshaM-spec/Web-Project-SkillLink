using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillLinkApp.Data;
using SkillLinkApp.Models;
using SkillLinkApp.Models.Interface;
using SkillLinkApp.Models.Repository;
namespace SkillLinkApp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepoInterface _customerRepo;
        private readonly IServiceRepoInterface _serviceRepo;
        private readonly IRequestRepoInterface _requestRepo;
        private readonly IRequestOfferRepoInterface _offerRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerController(
     UserManager<ApplicationUser> userManager,
     ICustomerRepoInterface customerRepo,
     IServiceRepoInterface serviceRepo,
     IRequestRepoInterface requestRepo,
     IRequestOfferRepoInterface offerRepo)
        {
            _userManager = userManager;
            _customerRepo = customerRepo;
            _serviceRepo = serviceRepo;
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


            Customer c = _customerRepo.GetCustomerById(user.Id);

            return View(c);
        }
        public async Task<IActionResult> Offers(int requestId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Index", "Home");

            var customer = _customerRepo.GetCustomerById(user.Id);

            if (customer == null)
                return NotFound();

            var request = _requestRepo.GetRequestById(requestId.ToString());

            if (request == null || request.CustomerId != customer.Id)
                return NotFound();

            var offers = _offerRepo.GetOffersByRequestId(requestId);

            ViewBag.RequestId = requestId;

            return View(offers);
        }
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult MaintenanceServices()
        {
            var services = _serviceRepo.GetServicesByType("Maintenance");

            return View(services);
        }

        public IActionResult CleaningServices()
        {
            var services = _serviceRepo.GetServicesByType("Cleaning");

            return View(services);
        }

        public IActionResult ConstructionServices()
        {
            var services = _serviceRepo.GetServicesByType("Construction");

            return View(services);
        }
        [HttpGet]
        public IActionResult CreateRequest(int serviceId)
        {
            var service = _serviceRepo.GetServiceById(serviceId.ToString());

            if (service == null)
            {
                return NotFound();
            }

            var request = new ServiceRequest
            {
                ServiceId = serviceId
            };

            ViewBag.ServiceName = service.ServiceName;

            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(ServiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    if (error.Value.Errors.Count > 0)
                    {
                        Console.WriteLine("Property: " + error.Key);

                        foreach (var e in error.Value.Errors)
                        {
                            Console.WriteLine("Error: " + e.ErrorMessage);
                        }
                    }
                }

                var service = _serviceRepo.GetServiceById(request.ServiceId.ToString());
                ViewBag.ServiceName = service?.ServiceName;

                return View(request);
            }

            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Get the customer's record
            var customer = _customerRepo.GetCustomerById(user.Id);

            if (customer == null)
            {
                return NotFound();
            }

            // Assign values automatically
            request.CustomerId = customer.Id;
            request.Status = "Pending";
            request.CreatedAt = DateTime.Now;

            // Save through the repository
            _requestRepo.AddRequest(request);

            return RedirectToAction("Home");
        }
        public async Task<IActionResult> MyBookings()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Index", "Home");

            var customer = _customerRepo.GetCustomerById(user.Id);

            if (customer == null)
                return NotFound();

            var bookings = _requestRepo.GetBookingsByCustomerId(customer.Id);

            return View(bookings);
        }
    }

}
