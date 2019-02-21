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

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRequestViewModel model){

                //TODO: create req model from the view model

                // add to the context

                // save

//return RedirectToAction()
                return Content("Post method create");

        }

        [HttpPost]
        public IActionResult Update(CreateRequestViewModel model, RequestModel request)
        {
            request.Id = 1; //Testing purposes only. Won't need once we pass a request through to it.
            request.PickupAddress = new Identity.Address
            {
                StreetNumber = model.PickupStreetNumber,
                StreetName = model.PickupStreetName,
                City = model.PickupCity,
                State = model.PickupState,
                ZipCode = model.PickupZipcode
            };

            request.DropOffAddress = new Identity.Address
            {
                StreetNumber = model.DropoffStreetNumber,
                StreetName = model.DropoffStreetName,
                City = model.DropoffCity,
                State = model.DropoffState,
                ZipCode = model.DropoffZipcode
            };

            request.Item = model.Item;
            request.PickUpInstructions = model.PickupInstructions;
            request.DropOffInstructions = model.DropoffInstructions;

            _context.Update<RequestModel>(request);
            _context.SaveChanges();

            return RedirectToAction("ConfirmUpdate");
        }

        public IActionResult ConfirmUpdate()
        {
            return View();
        }
    }
}
