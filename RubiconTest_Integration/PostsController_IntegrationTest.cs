using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using RubiconTest;
using RubiconTest.Core.Entities;
using RubiconTest.Infrastructure.EF;
using RubiconTest.Infrastructure.Models;
using RubiconTest.Infrastructure.Shared;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
            .UseSerilog()
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
            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts/" + blog.Slug);
            var response = await Client.ExecuteGetAsync(Request);


            // Assert
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);

        }


        [Fact]
        public async Task GetBlog_Should_Return_NotFound_Slug_Not_Found()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);


            //Act
            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts/" +"seersresressreersresersr");
            var response = await Client.ExecuteGetAsync(Request);

            // Assert
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);


        }

        #endregion


        #region GetBlogsTesting


        [Fact]
        public async Task GetBlogs_Should_Return_Ok_With_BlogList()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);


            //Act
            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts");


            var response = await Client.ExecuteGetAsync(Request);
            BlogsModel blogsModel= new JsonDeserializer().Deserialize<BlogsModel>(response);



            // Assert
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);
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
            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts" + "?tag=" + tag.Name);


            var response = await Client.ExecuteGetAsync(Request);
            BlogsModel blogsModel = new JsonDeserializer().Deserialize<BlogsModel>(response);



            // Assert
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);
            Assert.True(blogsModel != null);


        }

        #endregion

        #region AddBlogTesting

        [Fact]
        public async Task AddBlog_Return_Ok_With_Added_Blog()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            RubiconContext db = _server.Services.GetService(typeof(RubiconContext)) as RubiconContext;
            Tag tag = db.Tags.Take(1).First();
            DataGeneration generator = new DataGeneration();


            AddBlogModel addBlogModel = new AddBlogModel()
            {
                Body = "ttttt",
                Description = "tttt",
                TagList = new List<string>(),
                Title = generator.GenerateStringRandomLenght(10, 30)
            };
            addBlogModel.TagList.Add(tag.Name);

            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts").AddJsonBody(addBlogModel);


            //Act
            var response = await Client.ExecutePostAsync(Request);
            BlogModel blogsModel = new JsonDeserializer().Deserialize<BlogModel>(response);


            //Arrange
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);
            Assert.True(blogsModel!=null);

        }


        [Fact]
        public async Task AddBlog_Return_BadRequest_NameSlug_Already_Taken()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            RubiconContext db = _server.Services.GetService(typeof(RubiconContext)) as RubiconContext;
            Blog blog = db.Blogs.Take(1).First(); //get a random blog that is allready taken 


            AddBlogModel addBlogModel = new AddBlogModel()
            {
                Body = "ttttt",
                Description = "tttt",
                TagList = new List<string>(),
                Title = blog.Title
            };

            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts").AddJsonBody(addBlogModel);


            //Act
            var response = await Client.ExecutePostAsync(Request);


            //Arrange
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);

        }
        
        [Fact]
        public async Task AddBlog_Return_BadRequest_BadModel()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            AddBlogModel addBlogModel = new AddBlogModel()
            {
                Body = "ttttt",
                Description = "tttt",
                TagList = new List<string>(),
                Title =null
            };

            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts").AddJsonBody(addBlogModel);


            //Act
            var response = await Client.ExecutePostAsync(Request);


            //Arrange
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);

        }


        #endregion

        #region UpdateBlogTesting

        [Fact]
        public async Task UpdateBlog_Return_BadRequest_BadModel()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            UpdateBlogModel addBlogModel = new UpdateBlogModel()
            {
                Body = null,
                Description = null,
                Title = null
            };

            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts",Method.PUT).AddJsonBody(addBlogModel);


            //Act
            var response = await Client.ExecuteAsync(Request);

            //Arrange
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);
        }

        [Fact]
        public async Task UpdateBlog_Return_Ok_Return_UpdatedBlog()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            RubiconContext db = _server.Services.GetService(typeof(RubiconContext)) as RubiconContext;
            Blog blog = db.Blogs.Take(1).First(); //get a random blog that is allready taken 
            DataGeneration data = new DataGeneration();

            UpdateBlogModel addBlogModel = new UpdateBlogModel()
            {
                Slug = blog.Slug,
                Body = data.GenerateStringRandomLenght(10,30),
                Description = data.GenerateStringRandomLenght(10, 30),
                Title = data.GenerateStringRandomLenght(10, 30)
            };

            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts", Method.PUT).AddJsonBody(addBlogModel);

            //Act
            var response = await Client.ExecuteAsync(Request);
            BlogModel blogModel = new JsonDeserializer().Deserialize<BlogModel>(response);


            //Arrange
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);
            Assert.True(blogModel != null);
        }

        #endregion

        #region DeleteBlog

        [Fact]
        public async Task DeleteBlog_Return_Ok()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            RubiconContext db = _server.Services.GetService(typeof(RubiconContext)) as RubiconContext;
            Blog blog = db.Blogs.Take(1).First(); //get a blog to delete

          

            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/Posts/"+blog.Slug, Method.DELETE);

            //Act
            var response = await Client.ExecuteAsync(Request);


            //Arrange
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);
        }



        #endregion




    }
}
