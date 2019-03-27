using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DataProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        /// <summary>
        /// This method will take a request and inserts it into the database
        /// </summary>
        /// <returns></returns>
        [HttpPost("Insert")]
        public JsonResult Insert(JObject request)
        {
            return null;
        }

        /// <summary>
        /// This method takes an id of a request and returns the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public JsonResult GetById(int id)
        {
            return null;
        }


        /// <summary>
        /// This method takes an id of a request and deletes it from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public JsonResult Delete(int id)
        {
            return null;
        }

        [HttpPut("Update")]
        public JsonResult Update(JObject request)
        {
            return null;
        }

        /// <summary>
        /// This method will take a user guid id and return all requests made by them
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetAllByUserId")]
        public JsonResult GetAllByUserId(string id)
        {
            return null;
        }

    }
}