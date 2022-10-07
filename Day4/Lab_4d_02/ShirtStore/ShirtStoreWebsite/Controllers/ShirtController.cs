using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShirtStoreWebsite.Models;
using ShirtStoreWebsite.Services;
using System;

namespace ShirtStoreWebsite.Controllers
{
    public class ShirtController : Controller
    {
        private IShirtRepository _shirtRepository;
        private ILogger<ShirtController> _logger;

        public ShirtController(IShirtRepository shirtRepository, ILogger<ShirtController> logger)
        {
            _shirtRepository = shirtRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var shirts = _shirtRepository.GetShirts();
            return View(shirts);
        }

        public IActionResult AddShirt(Shirt shirt)
        {
            _shirtRepository.AddShirt(shirt);
            _logger.LogDebug($"A {shirt.Color} shirt of size {shirt.Size} with a price of {shirt.GetFormattedTaxedPrice()} was added successfully");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _shirtRepository.RemoveShirt(id);
                _logger.LogDebug($"A shirt with id {id} was removed successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while trying to delete shirt with id {id}.");
                throw ex;
            }
        }
    }
}