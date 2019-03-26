using Authentication2.DataAccessLayer;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Authentication2.Areas.Driver.Controllers
{
    [Area("Driver")]
    [Authorize(Roles = "Driver")]
    public class RequestController : Controller
    {

        private readonly IDbContext _context;

        public RequestController(IDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AcceptedRequests()
        {
            List<RequestModel> requests = _context.GetRequests();

            List<CreateRequestViewModel> requestsView = new List<CreateRequestViewModel> { };
            foreach (RequestModel model in requests)
            {
                if (model.DriverId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    requestsView.Add(new CreateRequestViewModel(model));
            }
            return View(requestsView);
        }

        public IActionResult UpdateStatus(int id)
        {
            RequestModel request = _context.GetRequestById(id);

            // TODO: return a view instead of contenet
            if (request == null)
                return Content("Request with given id does not exist.");

            CreateRequestViewModel requestVM = new CreateRequestViewModel(request);

            return View(requestVM);
        }

        [HttpPost]
        public IActionResult UpdateStatus(CreateRequestViewModel model)
        {
            RequestModel request = _context.GetRequestById(model.Id);

            var status = model.Status;
            var newStatus = status;

            if (status == "Accepted By Driver")
                newStatus = "Awaiting Pickup";
            else if (status == "Awaiting Pickup")
                newStatus = "Out for Delivery";
            else if (status == "Out for Delivery")
                newStatus = "Delivered";

            request.UserId = model.UserId;
            request.DriverId = model.DriverId;
            request.Status = newStatus;
            request.PickupAddress.StreetNumber = model.PickupStreetNumber;
            request.PickupAddress.StreetName = model.PickupStreetName;
            request.PickupAddress.City = model.PickupCity;
            request.PickupAddress.State = model.PickupState;
            request.PickupAddress.ZipCode = model.PickupZipcode;
            request.DropOffAddress.StreetNumber = model.DropoffStreetNumber;
            request.DropOffAddress.StreetName = model.DropoffStreetName;
            request.DropOffAddress.City = model.DropoffCity;
            request.DropOffAddress.State = model.DropoffState;
            request.DropOffAddress.ZipCode = model.DropoffZipcode;
            request.Item = model.Item;
            request.PickUpInstructions = model.PickupInstructions;
            request.DropOffInstructions = model.DropoffInstructions;

            _context.UpdateRequest(request);

            return RedirectToAction("AcceptedRequests");
        }

        public IActionResult Open()
        {
            List<RequestModel> requests = _context.GetRequests();

            List<CreateRequestViewModel> requestsView = new List<CreateRequestViewModel> { };
            foreach (RequestModel model in requests)
            {
                if (model.DriverId == null && model.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                    requestsView.Add(new CreateRequestViewModel(model));
            }
            return View(requestsView);
        }

        public IActionResult Pickup(int id)
        {
            RequestModel request = _context.GetRequestById(id);

            if (request == null)
                //TODO: Dont return content
                return Content("Request with given id does not exist.");

            CreateRequestViewModel requestVM = new CreateRequestViewModel(request);

            return View(requestVM);
        }

        [HttpPost]
        public IActionResult PickupRequest(CreateRequestViewModel model)
        {
            RequestModel request = _context.GetRequestById(model.Id);

            request.DriverId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.Status = "Accepted By Driver";

            _context.UpdateRequest(request);

            return RedirectToAction("ConfirmPickup");
        }

        public IActionResult ConfirmPickup()
        {
            return View();
        }
    }
}
