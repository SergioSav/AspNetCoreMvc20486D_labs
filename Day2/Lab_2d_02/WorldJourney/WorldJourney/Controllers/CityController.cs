using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WorldJourney.Models;
using WorldJourney.Filters;

namespace WorldJourney.Controllers
{
    public class CityController : Controller
    {
        private IData _data;
        private IHostingEnvironment _environment;

        public CityController(IData data, IHostingEnvironment environment)
        {
            _data = data;
            _environment = environment;

            _data.CityInitializeData();
        }

        [ServiceFilter(typeof(LogActionFilterAttribute))]
        [Route("WorldJourney")]
        public IActionResult Index()
        {
            ViewData["Page"] = "Search city";
            return View();
        }

        [Route("CityDetails/{id?}")]
        public IActionResult Details(int? id)
        {
            ViewData["Page"] = "Selected city";
            City city = _data.GetCityById(id);
            if (city == null)
                return NotFound();
            ViewBag.Title = city.CityName;
            return View(city);
        }

        public IActionResult GetImage(int? cityId)
        {
            ViewData["Message"] = "Display image";
            City requestedCity = _data.GetCityById(cityId);
            if (requestedCity != null)
            {
                var webRootPath = _environment.WebRootPath;
                var folderPath = "/images/";
                string fullPath = webRootPath + folderPath + requestedCity.ImageName;
                FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                byte[] fileBytes;
                using (BinaryReader br = new BinaryReader(fileOnDisk))
                {
                    fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                }
                return File(fileBytes, requestedCity.ImageMimeType);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
