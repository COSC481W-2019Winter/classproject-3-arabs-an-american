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


namespace Authentication2.Areas.Controllers
{
    [Area("User")]
    public class RequestController : Controller
    {

        private readonly MyIdentityContext _context;

        public RequestController(MyIdentityContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.addressList = GetAddressList();
                return View();
            }

            return Content("Please log in to use this feature");
        }

        [HttpPost]
        public IActionResult Create(CreateRequestViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                RequestModel request = new RequestModel
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Status = "Awaiting Driver",
                    PickupAddress = new Identity.Address
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        StreetNumber = model.PickupStreetNumber,
                        StreetName = model.PickupStreetName,
                        City = model.PickupCity,
                        State = model.PickupState,
                        ZipCode = model.PickupZipcode
                    },
                    DropOffAddress = new Identity.Address
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        StreetNumber = model.DropoffStreetNumber,
                        StreetName = model.DropoffStreetName,
                        City = model.DropoffCity,
                        State = model.DropoffState,
                        ZipCode = model.DropoffZipcode
                    },
                    Item = model.Item,
                    PickUpInstructions = model.PickupInstructions,
                    DropOffInstructions = model.DropoffInstructions,
                };

                // add to the context
                _context.Add(request);

                // save
                _context.SaveChanges();

                return RedirectToAction("ConfirmCreate");
            }
            return Content("Please log in to use this feature");
        }

        public IActionResult ConfirmCreate()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return Content("Please log in to use this feature");
        }

        public IActionResult Delete()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return Content("Please log in to use this feature");
        }

        public IActionResult ConfirmDelete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.id = id;
                return View();
            }

            return Content("Please log in to use this feature");
        }

        public IActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == 0)
                {
                    return Content("id is not workig");
                }
                var request = _context.Requests
                    .Where(req => req.Id == id)
                    .Include(req => req.DropOffAddress)
                    .Include(req => req.PickupAddress)
                    .FirstOrDefault();
                if (request != null)
                {
                    _context.Requests.Remove(request);
                    _context.SaveChanges();
                    ViewBag.id = id;
                    ViewBag.request = new CreateRequestViewModel(request);
                    return View();
                }
                return Content("ID does not exist: " + id);
            }

            return Content("Please log in to use this feature");
        }

        public IActionResult Detail(int? id)
        {
            if (User.Identity.IsAuthenticated)
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

            return Content("Please log in to use this feature");
        }

        public IActionResult Update(int? id)
        {
            if (User.Identity.IsAuthenticated)
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
                //new AddressController(_context).GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var pickUpAddressList = new List<SelectListItem> { };
                var dropOffAddressList = new List<SelectListItem> { };

                foreach (Address address in addresses)
                {
                    pickUpAddressList.Add(new SelectListItem
                    {
                        Value = address.StreetNumber,
                        Text = address.StreetName,
                        Selected = address.Id == request.PickupAddress.Id ? true : false
                    });

                    dropOffAddressList.Add(new SelectListItem
                    {
                        Value = address.StreetNumber,
                        Text = address.StreetName,
                        Selected = address.Id == request.DropOffAddress.Id ? true : false
                    });
                }

                Address[] addressArray = addresses.ToArray();

                ViewBag.pickUpAddressList = pickUpAddressList;
                ViewBag.dropOffAddressList = dropOffAddressList;
                ViewData["AddressArray"] = addressArray;
                return View(requestVM);
            }

            return Content("Please log in to use this feature");
        }

        [HttpPost]
        public IActionResult Update(CreateRequestViewModel request)
        {
            if (User.Identity.IsAuthenticated)
            {
                RequestModel existingRequest = _context.Requests
                    .Where(r => r.Id == request.Id)
                    .Include(req => req.DropOffAddress)
                    .Include(req => req.PickupAddress)
                    .FirstOrDefault();

                existingRequest.UserId = request.UserId;
                existingRequest.DriverId = request.DriverId;
                existingRequest.Status = request.Status;
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

                return RedirectToAction("ConfirmUpdate");
            }

            return Content("Please log in to use this feature");
        }

        public IActionResult ConfirmUpdate()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return Content("Please log in to use this feature");
        }

        public IActionResult List()
        {
            if (User.Identity.IsAuthenticated)
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

            return Content("Please log in to use this feature");
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
                    Text = "Saved Addresses",
                    Value = "0",
                    Selected = true
                }
            };

            foreach (Address address in addresses)
            {
                addressList.Add(new SelectListItem
                {
                    Value = address.Id.ToString(),
                    Text = address.StreetNumber + " " + address.StreetName,
                });
            }

            return addressList;
        }

    }
}