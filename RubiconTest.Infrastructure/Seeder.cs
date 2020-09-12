using RubiconTest.Core.Entities;
using RubiconTest.Infrastructure.EF;
using RubiconTest.Infrastructure.Services.BlogService;
using RubiconTest.Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiconTest.Infrastructure
{
    /// <summary>
    /// Class used for startup data generation if none is found 
    /// </summary>
    public  static  class Seeder
    {
        public static void SeedData(RubiconContext db)
        {

            //Check if there are any Tags if not generate some 
            var Tags = db.Tags.ToList();
            if (Tags.Count == 0)
            {
                List<Tag> tags = new List<Tag>();
                tags.Add(new Tag() { Name = "PC" });
                tags.Add(new Tag() { Name = "MacOS" });
                tags.Add(new Tag() { Name = "CV" });
                tags.Add(new Tag() { Name = "Work" });
                tags.Add(new Tag() { Name = "Android" });
                tags.Add(new Tag() { Name = "Deep learning" });

                db.AddRange(tags);
                db.SaveChanges();

            }


            //Check if there are Blogs if not generate some 
            var Blogs = db.Blogs.ToList();
            if (Blogs.Count == 0)
            {

                List<Tag> tags = db.Tags.ToList();

                Blog blog1 = new Blog()
                {
                    Title = "Why you should hire me!",
                    Description = "A well tough out joke",
                    Body = "You really should at least chuckle at this",
                    Slug = Slugefier.GetFriendlyTitle("Why you should hire me!"),

                };

                db.Blogs.Add(blog1); db.SaveChanges();

                List<BlogTag> blogTags1 = new List<BlogTag>();
                blogTags1.Add(new BlogTag()
                {
                    BlogSlug = blog1.Slug,
                    TagId = tags.SingleOrDefault(x => x.Name == "PC").Id
                });
                blogTags1.Add(new BlogTag()
                {
                    BlogSlug = blog1.Slug,
                    TagId = tags.SingleOrDefault(x => x.Name == "CV").Id
                });
                blogTags1.Add(new BlogTag()
                {
                    BlogSlug = blog1.Slug,
                    TagId = tags.SingleOrDefault(x => x.Name == "Work").Id
                });

                db.BlogTags.AddRange(blogTags1); db.SaveChanges();










                Blog blog2 = new Blog()
                {
                    Title = "I am running out of jokes after one blog",
                    Description = "The truth",
                    Body = "Kinda sad when you think about it",
                    Slug = Slugefier.GetFriendlyTitle("I am running out of jokes after one blog"),

                };

                db.Blogs.Add(blog2); db.SaveChanges();

                List<BlogTag> blogTags2 = new List<BlogTag>();
                blogTags2.Add(new BlogTag()
                {
                    BlogSlug = blog2.Slug,
                    TagId = tags.SingleOrDefault(x => x.Name == "PC").Id
                });
                blogTags2.Add(new BlogTag()
                {
                    BlogSlug = blog2.Slug,
                    TagId = tags.SingleOrDefault(x => x.Name == "Android").Id
                });
                blogTags2.Add(new BlogTag()
                {
                    BlogSlug = blog2.Slug,
                    TagId = tags.SingleOrDefault(x => x.Name == "Deep learning").Id
                });
                db.BlogTags.AddRange(blogTags2); db.SaveChanges();



                Blog blog3 = new Blog()
                {
                    Title = "Only 4 blogs are generated!",
                    Description = "true",
                    Body = "Small custom size is better than a lot of giberish data",
                    Slug = Slugefier.GetFriendlyTitle("Only 4 blogs are generated!"),

                };
                db.Blogs.Add(blog3); db.SaveChanges();

                List<BlogTag> blogTags3 = new List<BlogTag>();
                blogTags3.Add(new BlogTag()
                {
                    BlogSlug = blog3.Slug,
                    TagId = tags.SingleOrDefault(x => x.Name == "PC").Id
                });
                db.BlogTags.AddRange(blogTags3); db.SaveChanges();



                Blog blog4 = new Blog()
                {
                    Title = "A blog with no tags",
                    Description = "tags are not required ",
                    Body = "tags are not required ",
                    Slug = Slugefier.GetFriendlyTitle("A blog with no tags"),

                };
                db.Blogs.Add(blog4); db.SaveChanges();



            }


        }
    }
}
