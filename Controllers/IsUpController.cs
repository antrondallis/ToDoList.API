using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    public class IsUpController : Controller
    {
        [HttpGet, AllowAnonymous]
        public IActionResult IsUp()
        {
            return Ok("I AM.... ALIIIIVVVVE");
        }
    }
}