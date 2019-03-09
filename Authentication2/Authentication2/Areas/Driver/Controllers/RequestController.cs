using Authentication2.DataAccessLayer;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Authentication2.Areas.Driver.Controllers
{
    [Area("Driver")]
    public class RequestController : Controller
    {

        private readonly MyIdentityContext _context;

        public RequestController(MyIdentityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateStatus(int? id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Driver"))
            {
                RequestModel request = _context.Requests
                    .Where(r => r.Id == id)
                    .Include(req => req.DropOffAddress)
                    .Include(req => req.PickupAddress)
                    .FirstOrDefault();

                // TODO: return a view instead of contenet
                if (request == null)
                    return Content("Request with given id does not exist.");
                //TODO: refactor this to user the constructor
                CreateRequestViewModel requestVM = new CreateRequestViewModel
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    DriverId = request.DriverId,
                    Status = request.Status,
                    PickupStreetNumber = request.PickupAddress.StreetNumber,
                    PickupStreetName = request.PickupAddress.StreetName,
                    PickupCity = request.PickupAddress.City,
                    PickupState = request.PickupAddress.State,
                    PickupZipcode = request.PickupAddress.ZipCode,
                    PickupInstructions = request.PickUpInstructions,
                    DropoffStreetNumber = request.DropOffAddress.StreetNumber,
                    DropoffStreetName = request.DropOffAddress.StreetName,
                    DropoffCity = request.DropOffAddress.City,
                    DropoffState = request.DropOffAddress.State,
                    DropoffZipcode = request.DropOffAddress.ZipCode,
                    DropoffInstructions = request.DropOffInstructions,
                    Item = request.Item
                };

                return View(requestVM);
            }
            // TODO: use authorize attribute to get rid of this
            return Content("Please log in as a driver to use this feature");
        }

        [HttpPost]
        public IActionResult UpdateStatus(CreateRequestViewModel request)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Driver"))
            {
                RequestModel existingRequest = _context.Requests
                    .Where(r => r.Id == request.Id)
                    .Include(req => req.DropOffAddress)
                    .Include(req => req.PickupAddress)
                    .FirstOrDefault();

                var status = request.Status;
                var newStatus = status;

                if (status == "Accepted By Driver")
                    newStatus = "Awaiting Pickup";
                else if (status == "Awaiting Pickup")
                    newStatus = "Out for Delivery";
                else if (status == "Out for Delivery")
                    newStatus = "Delivered";

                existingRequest.UserId = request.UserId;
                existingRequest.DriverId = request.DriverId;
                existingRequest.Status = newStatus;
                existingRequest.PickupAddress.StreetNumber = request.PickupStreetNumber;
                existingRequest.PickupAddress.StreetName = request.PickupStreetName;
                existingRequest.PickupAddress.City = request.PickupCity;
                existingRequest.PickupAddress.State = request.PickupState;
                existingRequest.PickupAddress.ZipCode = request.PickupZipcode;
                existingRequest.DropOffAddress.StreetNumber = request.DropoffStreetNumber;
                existingRequest.DropOffAddress.StreetName = request.DropoffStreetName;
                existingRequest.DropOffAddress.City = request.DropoffCity;
                existingRequest.DropOffAddress.State = request.DropoffState;
                existingRequest.DropOffAddress.ZipCode = request.DropoffZipcode;
                existingRequest.Item = request.Item;
                existingRequest.PickUpInstructions = request.PickupInstructions;
                existingRequest.DropOffInstructions = request.DropoffInstructions;

                _context.Update(existingRequest);
                _context.SaveChanges();

                return RedirectToAction("ReadDriver");
            }
            //TODO: dont return content. Use authorize attribute to redirect to log in page
            return Content("Please log in as a driver to use this feature");
        }

        public IActionResult Open()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Driver"))
            {
                List<RequestModel> requests = _context.Requests
                    .Include(req => req.DropOffAddress)
                    .Include(req => req.PickupAddress)
                    .ToList();

                List<CreateRequestViewModel> requestsView = new List<CreateRequestViewModel> { };
                foreach (RequestModel model in requests)
                {
                    if (model.DriverId == null && model.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                        requestsView.Add(new CreateRequestViewModel(model));
                }
                return View(requestsView);
            }
            //TODO: dont return content. Use authorize attribute to redirect to log in page
            return Content("Please log in as a driver to use this feature");
        }

        public IActionResult PickupRequest(int? id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Driver"))
            {
                RequestModel request = _context.Requests
                    .Where(r => r.Id == id)
                    .Include(req => req.DropOffAddress)
                    .Include(req => req.PickupAddress)
                    .FirstOrDefault();

                if (request == null)
                    //TODO: Dont return content
                    return Content("Request with given id does not exist.");

                CreateRequestViewModel requestVM = new CreateRequestViewModel
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    DriverId = request.DriverId,
                    Status = request.Status,
                    PickupStreetNumber = request.PickupAddress.StreetNumber,
                    PickupStreetName = request.PickupAddress.StreetName,
                    PickupCity = request.PickupAddress.City,
                    PickupState = request.PickupAddress.State,
                    PickupZipcode = request.PickupAddress.ZipCode,
                    PickupInstructions = request.PickUpInstructions,
                    DropoffStreetNumber = request.DropOffAddress.StreetNumber,
                    DropoffStreetName = request.DropOffAddress.StreetName,
                    DropoffCity = request.DropOffAddress.City,
                    DropoffState = request.DropOffAddress.State,
                    DropoffZipcode = request.DropOffAddress.ZipCode,
                    DropoffInstructions = request.DropOffInstructions,
                    Item = request.Item
                };

                return View(requestVM);
            }
            //TODO: dont return content. Use authorize attribute to redirect to log in page
            return Content("Please log in as a driver to use this feature");
        }

        [HttpPost]
        public IActionResult PickupRequest(CreateRequestViewModel request)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Driver"))
            {
                RequestModel existingRequest = _context.Requests
                    .Where(r => r.Id == request.Id)
                    .Include(req => req.DropOffAddress)
                    .Include(req => req.PickupAddress)
                    .FirstOrDefault();

                existingRequest.UserId = request.UserId;
                existingRequest.DriverId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                existingRequest.Status = "Accepted By Driver";
                existingRequest.PickupAddress.StreetNumber = request.PickupStreetNumber;
                existingRequest.PickupAddress.StreetName = request.PickupStreetName;
                existingRequest.PickupAddress.City = request.PickupCity;
                existingRequest.PickupAddress.State = request.PickupState;
                existingRequest.PickupAddress.ZipCode = request.PickupZipcode;
                existingRequest.DropOffAddress.StreetNumber = request.DropoffStreetNumber;
                existingRequest.DropOffAddress.StreetName = request.DropoffStreetName;
                existingRequest.DropOffAddress.City = request.DropoffCity;
                existingRequest.DropOffAddress.State = request.DropoffState;
                existingRequest.DropOffAddress.ZipCode = request.DropoffZipcode;
                existingRequest.Item = request.Item;
                existingRequest.PickUpInstructions = request.PickupInstructions;
                existingRequest.DropOffInstructions = request.DropoffInstructions;

                _context.Update<RequestModel>(existingRequest);
                _context.SaveChanges();

                return RedirectToAction("ConfirmPickup");
            }
            //TODO: dont return content. Use authorize attribute to redirect to log in page
            return Content("Please log in as driver to use this feature");
        }

        public IActionResult ConfirmPickup()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Driver"))
                return View();
            //TODO: dont return content. Use authorize attribute to redirect to log in page
            return Content("Please log in as driver to use this feature");
        }
    }
}
