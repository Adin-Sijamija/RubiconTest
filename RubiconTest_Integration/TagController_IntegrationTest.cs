using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using RubiconTest;
using RubiconTest.Infrastructure.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RubiconTest_Integration
{
    public class TagController_IntegrationTest
    {


        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TagController_IntegrationTest()
        {
            //Server setup
            _server = new TestServer(new WebHostBuilder()
                .UseSerilog()
          .UseStartup<Startup>());
            _client = _server.CreateClient();
        }




        [Fact]
        public async Task Should_return_Json_With_Tag_List()
        {

            //// Arrange
            //HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);


            ////Act
            //var result =await _client.GetAsync("api/tags");

            //var stringResult = await result.Content.ReadAsStringAsync();
            //List<TagModel> models = JsonConvert.DeserializeObject<List<TagModel>>(stringResult);



            //// Assert
            //Assert.Equal(expectedRespone.StatusCode, result.StatusCode);
            //Assert.NotEmpty(models);


            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);


            //Act
            var Client = new RestClient("http://localhost:57595/");
            var Request = new RestRequest("api/tags");


            var response = await Client.ExecuteGetAsync(Request);
            List<TagModel> tags = new JsonDeserializer().Deserialize<List<TagModel>>(response);



            // Assert
            Assert.Equal(expectedRespone.StatusCode, response.StatusCode);
            Assert.True(tags != null);







        }
    }
}
