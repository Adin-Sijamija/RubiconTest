using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RubiconTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : Controller
    {


        //GET /api/tags

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
