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
    public class DishControllerTests
    {
        public readonly HttpClient _client;

        public DishControllerTests()
        {
            var server = new TestServer(new WebHostBuilder().UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetAllDishsTest(string method)
        {
            
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/dish/" );

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            
            var response = await _client.GetAsync("/api/dish/");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<DishModelView>>(await response.Content.ReadAsStringAsync());

            Assert.NotEmpty(models);
        }
        
        [Fact]
        public async Task GetDish_ReturnsNotFound()
        {

            var response = await _client.GetAsync("/api/dish/9890");
            
            
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public async Task TestPostDishItemAsync()
        {

            var request = new
            {
                Url = "/api/dish/",
                Body = new 
                {
                    Dishid = 101,
                    Name = "Dish For Test"
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
                Url = "/api/dish/",
                Body = new 
                {
                    Dishid = 1,
                    Name = "Dish For Test"
                }
            };
            
            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task DeleteMenuItemAsync()
        {
            var response = _client.DeleteAsync("/api/dish/1");

            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
        }
        
        
    }
}