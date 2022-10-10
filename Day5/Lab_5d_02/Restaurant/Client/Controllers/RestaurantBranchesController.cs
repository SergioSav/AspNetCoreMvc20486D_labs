using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http;
using Client.Models;
using System.Net.Http;
using System.Collections;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Client.Controllers
{
    public class RestaurantBranchesController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public RestaurantBranchesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new System.Uri("http://localhost:54517");
            var response = await httpClient.GetAsync("api/RestaurantBranches");
            if (response.IsSuccessStatusCode)
            {
                var restaurantBranches = JsonConvert.DeserializeObject<List<RestaurantBranch>>(await response.Content.ReadAsStringAsync());
                return View(restaurantBranches);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
