using Authentication2.DataAccessLayer;
using Authentication2.Identity;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Authentication2.Areas.Controllers
{
    [Area("User")]
    [Authorize]
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

        public IActionResult Create()
        {
            ViewBag.addressList = GetAddressList();
            ViewData["addresses"] = _context.Addresses
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToList()
                .ToArray();

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRequestViewModel model)
        {
            RequestModel request = new RequestModel
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Status = "Awaiting Driver",
                Item = model.Item,
                PickUpInstructions = model.PickupInstructions,
                DropOffInstructions = model.DropoffInstructions,
            };

            UpdatePickupAddress(model, request);
            UpdateDropoffAddress(model, request);

            // add to the context
            _context.Add(request);

            // save
            _context.SaveChanges();

            return RedirectToAction("ConfirmCreate", new CreateRequestViewModel(request));
        }


        public IActionResult ConfirmCreate(CreateRequestViewModel request)
        {
            ViewBag.request = request;

            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult ConfirmDelete(int id)
        {
            if (id == 0)
                return Content("Not a valid ID");

            var request = _context.Requests
                .Where(req => req.Id == id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

            ViewBag.request = new CreateRequestViewModel(request);

            return View();
        }

        public IActionResult DeleteConfirmed(int id)
        {
            if (id == 0)
                return Content("Not a valid ID");

            var request = _context.Requests
                .Where(req => req.Id == id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

            if (request != null)
            {
                _context.Requests.Remove(request);
                _context.SaveChanges();

                return RedirectToAction("List");
            }

            return Content("ID does not exist: " + request.Id);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var request = _context.Requests
                .Where(req => req.Id == id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

            if (request == null)
            {
                return Content("The model was null with ID: " + id.ToString());
            }

            CreateRequestViewModel requestVM = new CreateRequestViewModel(request);

            return View(requestVM);
        }

        public IActionResult Update(int? id)
        {
            RequestModel request = _context.Requests
                .Where(r => r.Id == id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

            if (request == null)
                return Content("Request with given id does not exist.");

            CreateRequestViewModel requestVM = new CreateRequestViewModel(request);

            var addresses = _context.Addresses
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToList();

            Address[] addressArray = addresses.ToArray();

            ViewBag.AddressList = GetAddressList();
            ViewData["AddressArray"] = addressArray;

            return View(requestVM);
        }

        [HttpPost]
        public IActionResult Update(CreateRequestViewModel model)
        {
            RequestModel request = _context.Requests
                .Where(r => r.Id == model.Id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

            request.UserId = model.UserId;
            request.DriverId = model.DriverId;
            request.Status = model.Status;
            request.Item = model.Item;
            request.PickUpInstructions = model.PickupInstructions;
            request.DropOffInstructions = model.DropoffInstructions;

            UpdatePickupAddress(model, request);
            UpdateDropoffAddress(model, request);

            _context.Update<RequestModel>(request);
            _context.SaveChanges();

            return RedirectToAction("ConfirmUpdate", new CreateRequestViewModel(request));
        }

        public IActionResult ConfirmUpdate(CreateRequestViewModel request)
        {
            ViewBag.request = request;

            return View();
        }

        public IActionResult List()
        {
            List<RequestModel> requests = _context.Requests
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .ToList();

            List<CreateRequestViewModel> requestsView = new List<CreateRequestViewModel> { };
            foreach (RequestModel model in requests)
            {
                if (model.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    requestsView.Add(new CreateRequestViewModel(model));
            }
            return View(requestsView);
        }

        private List<SelectListItem> GetAddressList()
        {
            var addresses = _context.Addresses
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToList();

            var addressList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Address",
                    Value = "0",
                    Selected = true
                }
            };

            foreach (Address address in addresses)
            {
                addressList.Add(new SelectListItem
                {
                    Value = address.Id.ToString(),
                    Text = address.StreetNumber + " "
                        + address.StreetName + ", "
                        + address.City + ", "
                        + address.State + " "
                        + address.ZipCode,
                });
            }

            return addressList;
        }

        private void UpdateDropoffAddress(CreateRequestViewModel model, RequestModel request)
        {
            if (_context.Addresses.Any(
                                x => x.UserId == request.UserId
                                && x.StreetNumber == model.DropoffStreetNumber
                                && x.StreetName == model.DropoffStreetName
                                && x.City == model.DropoffCity
                                && x.State == model.DropoffState
                                && x.ZipCode == model.DropoffZipcode))
            {
                request.DropOffAddressId = _context.Addresses
                    .Where(
                        x => x.UserId == request.UserId
                        && x.StreetNumber == model.DropoffStreetNumber
                        && x.StreetName == model.DropoffStreetName
                        && x.City == model.DropoffCity
                        && x.State == model.DropoffState
                        && x.ZipCode == model.DropoffZipcode)
                    .FirstOrDefault()
                    .Id;
            }
            else
            {
                request.DropOffAddress = new Identity.Address
                {
                    UserId = request.UserId,
                    StreetNumber = model.DropoffStreetNumber,
                    StreetName = model.DropoffStreetName,
                    City = model.DropoffCity,
                    State = model.DropoffState,
                    ZipCode = model.DropoffZipcode
                };
            }
        }

        private void UpdatePickupAddress(CreateRequestViewModel model, RequestModel request)
        {
            if (_context.Addresses.Any(
                                x => x.UserId == request.UserId
                                && x.StreetNumber == model.PickupStreetNumber
                                && x.StreetName == model.PickupStreetName
                                && x.City == model.PickupCity
                                && x.State == model.PickupState
                                && x.ZipCode == model.PickupZipcode))
            {
                request.PickupAddressId = _context.Addresses
                    .Where(
                        x => x.UserId == request.UserId
                        && x.StreetNumber == model.PickupStreetNumber
                        && x.StreetName == model.PickupStreetName
                        && x.City == model.PickupCity
                        && x.State == model.PickupState
                        && x.ZipCode == model.PickupZipcode)
                    .FirstOrDefault()
                    .Id;
            }
            else
            {
                request.PickupAddress = new Identity.Address
                {
                    UserId = request.UserId,
                    StreetNumber = model.PickupStreetNumber,
                    StreetName = model.PickupStreetName,
                    City = model.PickupCity,
                    State = model.PickupState,
                    ZipCode = model.PickupZipcode
                };
            }
        }
    }
}
