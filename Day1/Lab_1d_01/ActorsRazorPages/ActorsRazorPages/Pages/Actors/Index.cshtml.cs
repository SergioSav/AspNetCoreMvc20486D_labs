using ActorsRazorPages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ActorsRazorPages.Pages.Actors
{
    public class IndexModel : PageModel
    {
        private IData _data;

        public IndexModel(IData data)
        {
            _data = data;
        }

        public IList<Actor> Actors { get; set; }

        public void OnGet()
        {
            Actors = _data.ActorsInitializeData();
        }
    }
}
