using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication2.DataAccessLayer;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Update(int? id)
        {
            if (id == null)
                return Content("Request with given id does not exist.");

            RequestModel request = _context.Requests
                .Where(r => r.Id == id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();

            CreateRequestViewModel requestVM = new CreateRequestViewModel();
            requestVM.Id = request.Id;
            requestVM.PickupStreetNumber = request.PickupAddress.StreetNumber;
            requestVM.PickupStreetName = request.PickupAddress.StreetName;
            requestVM.PickupCity = request.PickupAddress.City;
            requestVM.PickupState = request.PickupAddress.State;
            requestVM.PickupZipcode = request.PickupAddress.ZipCode;
            requestVM.PickupInstructions = request.PickUpInstructions;
            requestVM.DropoffStreetNumber = request.DropOffAddress.StreetNumber;
            requestVM.DropoffStreetName = request.DropOffAddress.StreetName;
            requestVM.DropoffCity = request.DropOffAddress.City;
            requestVM.DropoffState = request.DropOffAddress.State;
            requestVM.DropoffZipcode = request.DropOffAddress.ZipCode;
            requestVM.DropoffInstructions = request.DropOffInstructions;
            requestVM.Item = request.Item;

            return View(requestVM);
        }

        [HttpPost]
        public IActionResult Create(CreateRequestViewModel model){

            //TODO: create req model from the view model
            RequestModel request = new RequestModel
            {
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

        public async Task<IActionResult> ViewByID(int? id)
        {
            if(id == null){
                return View();
            }

            var request = await _context.Requests.FindAsync(id);

            if(request == null)
            {
                return Content("The model was null with ID: "+id.ToString());
            }
            // var request = new ViewByIDViewModel();
            // request.PickupAddress = new Identity.Address();
            // request.DropOffAddress = new Identity.Address();
            // request.Item = "Fishsticks";
            // request.PickUpInstructions = "eat it";
            // request.DropOffInstructions = "fish it";
            return View(request);
            
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

            return View(_context.Requests
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .ToList());
        }
    }
}
