﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RubiconTest.Infrastructure.Services.BlogService;
using RubiconTest.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace RubiconTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {

        private readonly IBlogService service;

        public PostsController(IBlogService service)
        {
            this.service = service;
        }



        //GET /api/posts/:slug
        [Route("{slug?}")]
        [HttpGet]
        public async Task<IActionResult> GetBlog([FromRoute] string slug)
        {

            var result = await service.GetBlog(slug);

            //Ultimately I would have redirected this to a not found page that recommends
            //similar/random articles but considering you only want end points this also works
            if (result == null)
            {
                return NotFound(string.Format(@"The blog you are looking for: {0} ,can't be found, make sure you entered the url properly", slug));
            }


            return Ok(result);
        }


        //GET /api/posts ?tag
        [HttpGet]
        public async Task<IActionResult> GetBlogs([FromQuery] string tag = null)
        {

            var result = await service.GetBlogs(tag);


            return Ok(result);
        }

        //POST /api/posts
        [HttpPost]
        public async Task<IActionResult> AddBlog([FromBody] AddBlogModel model)
        {


            if (!TryValidateModel(model))
            {
                var errors = ModelState.Select(x => new { x.Key, x.Value }).ToArray();

                return BadRequest(errors);
            }

            var result = await service.AddBlog(model);

            if (result == null)
            {
                return BadRequest("Selected title already taken. Please change it to something else");
            }

            return Ok(result);
        }

        //PUT /api/posts/:slug
        [HttpPut]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogModel model)
        {
          

            //Make sure we have something to update 
            if (model.Body == null && model.Description == null && model.Title == null)
                return BadRequest("Nothing to update!");

            var result = await service.UpdateBlog(model);

            if (result == null)
                BadRequest("Selected title is already taken");


            return Ok(result);
        }

        //DELETE /api/posts/:slug
        [HttpDelete]
        [Route("{slug?}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] string slug)
        {

            var result = await service.DeleteBlog(slug);

            if (result == false)
                return BadRequest(string.Format("Blog with the slug/url: {0} , does not exists",slug));


            return Ok("Blog successfully deleted");
        }



    }
}
