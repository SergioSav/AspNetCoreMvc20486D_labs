using Microsoft.AspNetCore.Mvc;

namespace PhotoSharingWebApp.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/addComment")]
        public IActionResult AddComment()
        {
            return Ok("addComment simulation");
        }

        [HttpPost("/api/addComment/{id}")]
        public IActionResult AddComment(int? id)
        {
            return Ok($"addComment simulation {id}");
        }

        [HttpGet("/api/showComments")]
        public IActionResult ShowComments()
        {
            return Ok("showComments simulation");
        }
    }
}
