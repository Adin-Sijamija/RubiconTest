using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RubiconTest.Controllers
{
    public class TagsController : Controller
    {


        //GET /api/tags


        public IActionResult Index()
        {
            return View();
        }
    }
}
