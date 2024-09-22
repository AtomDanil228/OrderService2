using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels2;
using System.Net.Http;
using System.Text.Json;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _userServiceUrl = "https://localhost:7240/api/user";
        private readonly string _productServiceUrl = "https://localhost:7053/api/product";

        private static readonly List<Order> Orders = new List<Order>();
        public MyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        private async Task<User?> GetUserById(int userId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_userServiceUrl}/{userId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(json);
        }
    }
}
