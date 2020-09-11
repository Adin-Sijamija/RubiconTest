using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RubiconTest.Core.Entities;
using RubiconTest.Infrastructure.EF;
using RubiconTest.Infrastructure.Models;
using RubiconTest.Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

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
            blog.Slug = Slugefier.GetFriendlyTitle(model.Title);
            List<string> tagNames = model.TagList;

            await db.Blogs.AddAsync(blog);
            await db.SaveChangesAsync();

            List<BlogTag> ValidBlogTags = new List<BlogTag>();
            List<Tag> Tags = await db.Tags.AsNoTracking().ToListAsync();
            foreach (var tag in tagNames)
            {
                //make sure each tag exsists 
                var exsists = Tags.FirstOrDefault(x => x.Name.ToLower().Trim() == tag.ToLower().Trim());

                //If it exists add it otherwise skip it
                if (exsists != null)
                {
                    ValidBlogTags.Add(new BlogTag() { TagId = exsists.Id, BlogSlug = blog.Slug });
                }

            }

            await db.BlogTags.BulkInsertAsync(ValidBlogTags);
            await db.SaveChangesAsync();


            //return the newly added blog with the updated navigation properties/tags
            Blog UpdatedBlog = await db.Blogs.AsNoTracking().Include(x => x.BlogTags).ThenInclude(x => x.Tag).FirstOrDefaultAsync(x => x.Slug == blog.Slug);
            return mapper.Map<BlogModel>(UpdatedBlog);

        }

        public async Task<BlogModel> GetBlog(string slug)
        {

            var result = Slugefier.GetFriendlyTitle(slug);

            var exists = db.Blogs.SingleOrDefault(x => x.Slug == slug);

            //if its not found return null
            if (exists == null)
                return null;

            return mapper.Map<BlogModel>(exists);

        }

        public async Task<BlogsModel> GetBlogs(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                //Mapp all blogs order by date with any tag
                List<BlogModel> blogModels = mapper.Map<List<BlogModel>>(await db.Blogs.AsNoTracking().Include(x=>x.BlogTags).ThenInclude(x=>x.Tag).OrderByDescending(x => x.UpdatedAt).ToListAsync());

                return new BlogsModel() { blogs = blogModels, PostsCount = blogModels.Count };

            }
            else
            {
                //make sure the tag exists
                Tag TagExists = await db.Tags.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == tag.ToLower().Trim());

                if (TagExists != null)
                {
                    //if it does exists mapp it to model
                    List<BlogModel> models = mapper.Map<List<BlogModel>>(await db.Blogs.AsNoTracking().Include(x => x.BlogTags).ThenInclude(x => x.Tag).Where(x => x.BlogTags.Where(y => y.Tag.Name == tag).Any()).OrderByDescending(x => x.UpdatedAt).ToListAsync());

                    return new BlogsModel() { blogs = models, PostsCount = models.Count };

                }
                else
                {
                    //if it doesnt return a empty array with 0 count
                    return new BlogsModel() { blogs = null, PostsCount = 0 };

                }


               
            }



        }




    }
}
