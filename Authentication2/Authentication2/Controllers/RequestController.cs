using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication2.DataAccessLayer;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;

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


    }
}
