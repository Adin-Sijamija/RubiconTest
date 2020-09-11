using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RubiconTest.Infrastructure.Services.BlogService;
using RubiconTest.Infrastructure.Models;

namespace RubiconTest.Controllers
{
    public class PostsController : Controller
    {

        private readonly IBlogService service;

        public PostsController(IBlogService service)
        {
            this.service = service;
        }


        //GET /api/posts/:slug
        [HttpGet]
        public async Task<BlogModel> GetBlog(string slug)
        {

            var result = await service.GetBlog(slug);



            return result;
        }

        //GET /api/posts ?tag
        [HttpGet]
        public async Task<string> GetBlogs(string tag=null)
        {
            return "";
        }

        //POST /api/posts
        [HttpPost]
        public async  Task<BlogModel> AddBlog([FromBody] AddBlogModel model)
        {

            var result = await service.AddBlog(model);

            return result;
        }

        //PUT /api/posts/:slug
        [HttpPut]
        public async Task<string> UpdateBlog(string slug)
        {
            return "";
        }

        //DELETE /api/posts/:slug
        [HttpDelete]
        public async Task<string> DeleteBlog(string slug)
        {
            return "";
        }



    }
}
