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
    public class AdressController : ControllerBase
    {
        /// <summary>
        /// Get a list of addresses for a user using HIS id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetByUserId(string id)
        {
            return null;
        }

        [HttpPost("Update")]
        public JsonResult Update(JObject address)
        {
            return null;
        }
    }
}