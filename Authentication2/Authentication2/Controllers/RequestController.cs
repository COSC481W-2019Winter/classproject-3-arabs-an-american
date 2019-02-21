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
        public int ID { get; set; }
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

                // add to the context

                // save

//return RedirectToAction()
                return Content("Post method create");

        }
      
    }
}
