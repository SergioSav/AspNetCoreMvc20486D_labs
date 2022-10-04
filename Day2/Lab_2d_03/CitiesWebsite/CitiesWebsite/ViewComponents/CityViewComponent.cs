using Microsoft.AspNetCore.Mvc;
using CitiesWebsite.Services;
using CitiesWebsite.Models;
using System.Threading.Tasks;

namespace CitiesWebsite.ViewComponents
{
	public class CityViewComponent : ViewComponent
	{
		private ICityProvider _cities;

		public CityViewComponent(ICityProvider cities)
		{
			_cities = cities;
		}

		public async Task<IViewComponentResult> InvokeAsync(string cityName)
		{
			ViewBag.CurrentCity = await GetCity(cityName);
			return View("_SelectCity");
		}

		private Task<City> GetCity(string cityName)
		{
			return Task.FromResult(_cities[cityName]);
		}
	}
}
