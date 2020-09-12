using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RubiconTest.Infrastructure.Services.TagService;

namespace RubiconTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : Controller
    {

        private readonly ITagService service;

        public TagsController(ITagService service)
        {
            this.service = service;
        }

        //GET /api/tags
        [HttpGet]
        public async  Task<IActionResult> getTags()
        {

            return Ok(await service.GetTags());

        }
    }
}
