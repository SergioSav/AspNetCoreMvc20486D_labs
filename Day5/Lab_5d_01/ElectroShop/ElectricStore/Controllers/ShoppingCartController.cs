using ElectricStore.Data;
using ElectricStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ElectricStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private StoreContext _context;
        private List<Product> _products;
        private SessionStateViewModel _sessionModel;

        public ShoppingCartController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CustomerProducts")))
            {
                var productListId = JsonConvert.DeserializeObject<int[]>(HttpContext.Session.GetString("CustomerProducts"));
                _products = new List<Product>();
                foreach (var item in productListId)
                {
                    var product = _context.Products.SingleOrDefault(p => p.Id == item);
                    _products.Add(product);
                }
                _sessionModel = new SessionStateViewModel();
                _sessionModel.CustomerName = HttpContext.Session.GetString("CustomerFirstName");
                _sessionModel.SelectedProducts = _products;
                return View(_sessionModel);
            }
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}