using ShirtStoreWebsite.Data;
using ShirtStoreWebsite.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShirtStoreWebsite.Services
{
    public class ShirtsRepository : IShirtRepository
    {
        private ShirtContext _context;

        public ShirtsRepository(ShirtContext context)
        {
            _context = context;
        }

        public bool AddShirt(Shirt shirt)
        {
            _context.Add(shirt);
            var entries = _context.SaveChanges();
            if (entries > 0)
                return true;
            return false;
        }

        public IEnumerable<Shirt> GetShirts()
        {
            return _context.Shirts.ToList();
        }

        public bool RemoveShirt(int id)
        {
            var shirt = _context.Shirts.SingleOrDefault(s => s.Id == id);
            _context.Remove(shirt);
            var entries = _context.SaveChanges();
            if (entries > 0)
                return true;
            return false;
        }
    }
}
