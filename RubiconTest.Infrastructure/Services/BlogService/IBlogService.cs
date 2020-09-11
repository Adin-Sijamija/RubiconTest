using RubiconTest.Core.Entities;
using RubiconTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RubiconTest.Infrastructure.Services.BlogService
{
    public interface IBlogService
    {
        Task<BlogModel> GetBlog(string slug);

        Task<BlogModel> AddBlog(AddBlogModel model);

        Task<BlogsModel> GetBlogs(string tag);
    }
}
