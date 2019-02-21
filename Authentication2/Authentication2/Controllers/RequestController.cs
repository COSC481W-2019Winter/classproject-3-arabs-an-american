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

        public IActionResult Update(long id)
        {
            id = 1;//Testing purposes only
            RequestModel request = _context.Requests.Where(r => r.Id == id).FirstOrDefault();
            return View(request);
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
        public IActionResult Update(RequestModel request)
        {
            RequestModel existingRequest = _context.Requests.Where(r => r.Id == request.Id).FirstOrDefault();

            existingRequest.PickupAddress = request.PickupAddress;
            existingRequest.DropOffAddress = request.DropOffAddress;
            existingRequest.Item = request.Item;
            existingRequest.PickUpInstructions = request.PickUpInstructions;
            existingRequest.DropOffInstructions = request.DropOffInstructions;

            //_context.Update<RequestModel>(request);
            _context.SaveChanges();

            return RedirectToAction("ConfirmUpdate");
        }

        public IActionResult ConfirmUpdate()
        {
            return View();
        }
    }
}
