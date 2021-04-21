using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using PresentationLayer;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization.Internal;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Tests
{
    public class MenuControllerTests
    {
        public readonly HttpClient _client;

        public MenuControllerTests()
        {
            var server = new TestServer(new WebHostBuilder().UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetAllMenuTest(string method)
        {
            
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/menu/" );

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            
            var response = await _client.GetAsync("/api/menu/");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<MenuModelView>>(await response.Content.ReadAsStringAsync());

            Assert.NotEmpty(models);
        }
        
        [Fact]
        public async Task GetMenu_ReturnsNotFound()
        {

            var response = await _client.GetAsync("/api/menu/9890");
            
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public async Task TestPostMenuItemAsync()
        {

            var request = new
            {
                Url = "/api/menu/",
                Body = new 
                {
                    Positionid = 33,
                    Dishid = 20,
                    Size = 1234,
                    Price =  3400
                }
            };


            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            
            response.EnsureSuccessStatusCode();
        }
        
        
        [Fact]
        public async Task TestPutMenuItemAsync()
        {
            var request = new
            {
                Url = "/api/menu/",
                Body = new 
                {
                    Positionid = 5,
                    Dishid = 2,
                    Size = 1234,
                    Price =  3400
                }
            };
            
            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            response.EnsureSuccessStatusCode();
        }
    }
}