using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RubiconTest;
using RubiconTest.Core.Entities;
using RubiconTest.Infrastructure.EF;
using RubiconTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RubiconTest_Integration
{
    public class PostsController_IntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PostsController_IntegrationTest()
        {
            //Server setup
            _server = new TestServer(new WebHostBuilder()
          .UseStartup<Startup>());
            _client = _server.CreateClient();
        }



        #region GetBlogBySlugTesting

        //GetBlogBySlugTesting
        [Fact]
        public async Task GetBlog_Should_Return_OK_With_Model()
        {


            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            //get the ef context to make sure we get a actual blog 
            RubiconContext db = _server.Services.GetService(typeof(RubiconContext)) as RubiconContext;
            Blog blog = db.Blogs.Take(1).First();

            //Act
            var result = await _client.GetAsync("api/posts/" + blog.Slug);

            var stringResult = await result.Content.ReadAsStringAsync();
            BlogModel blogModel = JsonConvert.DeserializeObject<BlogModel>(stringResult);



            // Assert
            Assert.Equal(expectedRespone.StatusCode, result.StatusCode);
            Assert.True(blogModel != null);

        }


        [Fact]
        public async Task GetBlog_Should_Return_NotFound_Slug_Not_Found()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);


            //Act
            var result = await _client.GetAsync("api/posts/" + "sfesfesfesfesfesfse");


            // Assert
            Assert.Equal(expectedRespone.StatusCode, result.StatusCode);


        }

        #endregion


        #region GetBlogsTesting


        [Fact]
        public async Task GetBlogs_Should_Return_Ok_With_BlogList()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);


            //Act
            var result = await _client.GetAsync("api/posts");

            var stringResult = await result.Content.ReadAsStringAsync();
            BlogsModel blogsModel = JsonConvert.DeserializeObject<BlogsModel>(stringResult);



            // Assert
            Assert.Equal(expectedRespone.StatusCode, result.StatusCode);
            Assert.True(blogsModel != null);

        }

        [Fact]
        public async Task GetBlogs_With_Specific_Tag_Should_Return_Ok_With_BlogList()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            RubiconContext db = _server.Services.GetService(typeof(RubiconContext)) as RubiconContext;
            Tag tag = db.Tags.Include(x => x.BlogTags).Where(x => x.BlogTags != null).Take(1).First();//get a tag that has atleast 1 blog 

            //Act
            var result = await _client.GetAsync("api/posts" + "?tag=" + tag.Name);

            var stringResult = await result.Content.ReadAsStringAsync();
            BlogsModel blogsModel = JsonConvert.DeserializeObject<BlogsModel>(stringResult);



            // Assert
            Assert.Equal(expectedRespone.StatusCode, result.StatusCode);
            Assert.True(blogsModel != null);

        }

        #endregion

        #region AddBlogTesting

        [Fact]
        public async Task AddBlog_Return_BadRequest_BadModel()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            RubiconContext db = _server.Services.GetService(typeof(RubiconContext)) as RubiconContext;
            Tag tag = db.Tags.Take(1).First();

            AddBlogModel model = new AddBlogModel()
            {
                Body = "Integration test",
                Description = "Integration test",
                TagList = new List<string>(),
                Title = "Integration test"
            };
            model.TagList.Add(tag.Name);
            var content = new StringContent(model.ToString(), Encoding.UTF8, "application/json");

            //Act
            var result = await _client.PostAsync("api/posts",content);

            var stringResult = await result.Content.ReadAsStringAsync();
            BlogModel blogModel = JsonConvert.DeserializeObject<BlogModel>(stringResult);



            // Assert
            Assert.Equal(expectedRespone.StatusCode, result.StatusCode);
            Assert.True(blogModel != null);

        }


        [Fact]
        public async Task AddBlog_Return_BadRequest_NameSlug_Already_Taken()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            //Act

            // Assert

        }

        [Fact]
        public async Task AddBlog_Return_Ok_With_Added_Blog()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            //Act

            // Assert

        }


        #endregion

        #region UpdateBlogTesting

        //bad request model empty

        //ok


        #endregion

        #region DeleteBlog

        //bad request not found

        //ok
        #endregion




    }
}
