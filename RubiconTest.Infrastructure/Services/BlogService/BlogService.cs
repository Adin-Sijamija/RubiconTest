using AutoMapper;
using RubiconTest.Core.Entities;
using RubiconTest.Infrastructure.EF;
using RubiconTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiconTest.Infrastructure.Services.BlogService
{
    public class BlogService : IBlogService
    {

        private readonly RubiconContext db;
        private readonly IMapper mapper;

        public BlogService(RubiconContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<BlogModel> AddBlog(AddBlogModel model)
        {

            Blog blog = mapper.Map<Blog>(model);

            await db.Blogs.AddAsync(blog);
            await db.SaveChangesAsync();

            return mapper.Map<BlogModel>(blog);

        }

        public async Task<BlogModel> GetBlog(string slug)
        {

            throw new NotImplementedException();



            //var exsists = db.Blogs.SingleOrDefault(x => x.Slug == slug);

            //if (exsists == null)
            //    return null;

            //return exsists;

        }

      
    }
}
