using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RubiconTest;
using RubiconTest.Infrastructure.Models;
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
          .UseStartup<Startup>());
            _client = _server.CreateClient();
        }




        [Fact]
        public async Task Should_return_Json_With_Tag_List()
        {

            // Arrange
            HttpResponseMessage expectedRespone = new HttpResponseMessage(System.Net.HttpStatusCode.OK);


            //Act
            var result =await _client.GetAsync("api/tags");

            var stringResult = await result.Content.ReadAsStringAsync();
            List<TagModel> models = JsonConvert.DeserializeObject<List<TagModel>>(stringResult);



            // Assert
            Assert.Equal(expectedRespone.StatusCode, result.StatusCode);
            Assert.NotEmpty(models);

        }
    }
}
