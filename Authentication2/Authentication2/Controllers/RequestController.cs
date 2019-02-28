using Authentication2.DataAccessLayer;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication2.Controllers
{
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
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult ConfirmDelete(int id)
        {
            ViewBag.id = id;
            return View();
        }     
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0){
                return Content("id is not workig");
            }
            var request = await _context.Requests.FindAsync(id);
            if (request != null){
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return Content("Request with id is deleted: " + id);
            }
            return Content("ID does not exist: " + id);
            
        }
       

        [HttpPost]
        public IActionResult Create(CreateRequestViewModel model){

            //TODO: create req model from the view model
            RequestModel request = new RequestModel
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                PickupAddress = new Identity.Address
                {
                    StreetNumber = model.PickupStreetNumber,
                    StreetName = model.PickupStreetName,
                    City = model.PickupCity,
                    State = model.PickupState,
                    ZipCode = model.PickupZipcode
                },
                DropOffAddress = new Identity.Address
                {
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
            _context.Add<RequestModel>(request);

            // save
            _context.SaveChanges();

            return RedirectToAction("ConfirmCreate");
            //return Content(model.Item);
        }

        public IActionResult ConfirmCreate()
        {
            return View();
        }

        public IActionResult ViewByID(int? id)
        {
            if(id == null){
                return View();
            }

            var request = _context.Requests
                .Where(req => req.Id == id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

            if (request == null)
            {
                return Content("The model was null with ID: "+id.ToString());
            }

            CreateRequestViewModel requestVM = new CreateRequestViewModel
            {
                Id = request.Id,
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

        public IActionResult Update(int? id)
        {
            RequestModel request = _context.Requests
                .Where(r => r.Id == id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

            if (request == null)
                return Content("Request with given id does not exist.");

            CreateRequestViewModel requestVM = new CreateRequestViewModel
            {
                Id = request.Id,
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

        [HttpPost]
        public IActionResult Update(CreateRequestViewModel request)
        {
            RequestModel existingRequest = _context.Requests
                .Where(r => r.Id == request.Id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

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

        public IActionResult ConfirmUpdate()
        {
            return View();
        }

        public IActionResult ReadUser()
        {
            List<RequestModel> requests = _context.Requests
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .ToList();

            List<CreateRequestViewModel> requestsView = new List<CreateRequestViewModel> { };
            foreach (RequestModel model in requests)
            {
                requestsView.Add(new CreateRequestViewModel(model));
            }
            return View(requestsView);
        }
      
    }
}
