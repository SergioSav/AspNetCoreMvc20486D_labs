using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class JobApplicationController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public JobApplicationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Create()
        {
            await PopulateEmployeeRequirementsDropDownListAsync();
            return View();
        }

        private async Task PopulateEmployeeRequirementsDropDownListAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:54517");
            var response = await httpClient.GetAsync("api/RestaurantWantedAd");
            if (response.IsSuccessStatusCode)
            {
                var employeeRequirements = JsonConvert.DeserializeObject<List<EmployeeRequirements>>(await response.Content.ReadAsStringAsync());
                ViewBag.EmployeeRequirements = new SelectList(employeeRequirements, "Id", "JobTitle");
            }
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
