using Authentication2.DataAccessLayer;
using Authentication2.Identity;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Authentication2.Areas.Controllers
{
    [Area("User")]
    [Authorize]
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

        public IActionResult Create()
        {
            ViewBag.addressList = GetAddressList();
            ViewData["addresses"] = _context
                .GetUserAddresses(User.FindFirstValue(ClaimTypes.NameIdentifier))
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
            _context.AddRequest(request);

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
                //return RedirectToPage("/Views/Shared/test");

            var request = _context.GetRequestById(id);

            ViewBag.request = new CreateRequestViewModel(request);

            return View();
        }

        public IActionResult DeleteConfirmed(int id)
        {
            if (id == 0)
                return Content("Not a valid ID");

            var request = _context.GetRequestById(id);

            if (request != null)
            {
                _context.RemoveRequest(request);

                return RedirectToAction("List");
            }

            return Content("ID does not exist: " + request.Id);
        }

        public IActionResult Detail(int id)
        {
            var request = _context.GetRequestById(id);

            if (request == null)
            {
                return Content("The model was null with ID: " + id.ToString());
            }

            CreateRequestViewModel requestVM = new CreateRequestViewModel(request);

            return View(requestVM);
        }

        public IActionResult Update(int id)
        {
            RequestModel request = _context.GetRequestById(id);

            if (request == null)
                return Content("Request with given id does not exist.");

            CreateRequestViewModel requestVM = new CreateRequestViewModel(request);

            var addresses = _context
                .GetUserAddresses(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Address[] addressArray = addresses.ToArray();

            ViewBag.AddressList = GetAddressList();
            ViewData["AddressArray"] = addressArray;

            return View(requestVM);
        }

        [HttpPost]
        public IActionResult Update(CreateRequestViewModel model)
        {
            RequestModel request = _context.GetRequestById(model.Id);

            request.UserId = model.UserId;
            request.DriverId = model.DriverId;
            request.Status = model.Status;
            request.Item = model.Item;
            request.PickUpInstructions = model.PickupInstructions;
            request.DropOffInstructions = model.DropoffInstructions;

            UpdatePickupAddress(model, request);
            UpdateDropoffAddress(model, request);

            _context.UpdateRequest(request);

            return RedirectToAction("ConfirmUpdate", new CreateRequestViewModel(request));
        }

        public IActionResult ConfirmUpdate(CreateRequestViewModel request)
        {
            ViewBag.request = request;

            return View();
        }

        public IActionResult List()
        {
            List<RequestModel> requests = _context.GetRequests();

            List<CreateRequestViewModel> requestsView = new List<CreateRequestViewModel> { };
            foreach (RequestModel model in requests)
            {
                if (model.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    requestsView.Add(new CreateRequestViewModel(model));
            }
            return View(requestsView);
        }

        public List<SelectListItem> GetAddressList()
        {
            var addresses = _context.GetUserAddresses(User.FindFirstValue(ClaimTypes.NameIdentifier));

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

        public void UpdateDropoffAddress(CreateRequestViewModel model, RequestModel request)
        {
            var address = new Address
            {
                UserId = request.UserId,
                StreetNumber = model.DropoffStreetNumber,
                StreetName = model.DropoffStreetName,
                City = model.DropoffCity,
                State = model.DropoffState,
                ZipCode = model.DropoffZipcode
            };
            if (_context.IfExistingAddress(address))
            {
                request.DropOffAddressId = _context.GetAddressId(address);
            }
            else
            {
                request.DropOffAddress = new Address
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

        public void UpdatePickupAddress(CreateRequestViewModel model, RequestModel request)
        {
            var address = new Address
            {
                UserId = request.UserId,
                StreetNumber = model.PickupStreetNumber,
                StreetName = model.PickupStreetName,
                City = model.PickupCity,
                State = model.PickupState,
                ZipCode = model.PickupZipcode
            };
            if (_context.IfExistingAddress(address))
            {
                request.PickupAddressId = _context.GetAddressId(address);
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
