using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using Client.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Client.Controllers
{
    public class ReservationController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateRestaurantBranchesDropDownListAsync();
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePostAsync(OrderTable orderTable)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(orderTable);
            var response = await httpClient.PostAsync("http://localhost:54517/api/Reservation", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderTable>(await response.Content.ReadAsStringAsync());
                return RedirectToAction("ThankYouAsync", new { orderId = order.Id});
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ThankYouAsync(int orderId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new System.Uri("http://localhost:54517");
            var response = await httpClient.GetAsync("api/Reservation/" + orderId);
            if (response.IsSuccessStatusCode)
            {
                var orderResult = JsonConvert.DeserializeObject<OrderTable>(await response.Content.ReadAsStringAsync());
                return View(orderResult);
            }
            else
            {
                return View("Error");
            }
        }

        private async Task PopulateRestaurantBranchesDropDownListAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new System.Uri("http://localhost:54517");
            var response = await httpClient.GetAsync("api/RestaurantBranches");
            if (response.IsSuccessStatusCode)
            {
                var restaurantBranches = JsonConvert.DeserializeObject<List<RestaurantBranch>>(await response.Content.ReadAsStringAsync());
                ViewBag.RestaurantBranches = new SelectList(restaurantBranches, "Id", "City");
            }
        }
    }
}
