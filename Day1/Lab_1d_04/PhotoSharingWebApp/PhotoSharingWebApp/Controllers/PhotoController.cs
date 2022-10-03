using Microsoft.AspNetCore.Mvc;

namespace PhotoSharingWebApp.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/uploadPhoto")]
        public IActionResult UploadPhoto()
        {
            return Ok("upload simulation");
        }

        [HttpPost("/api/uploadFile/{file}")]
        public IActionResult UploadPhoto(byte[] file)
        {
            return Ok($"upload simulation {file.Length}");
        }

        [HttpGet("/api/deletePhoto")]
        public IActionResult DeletePhoto()
        {
            return Ok("delete simulation");
        }

        [HttpPost("/api/deletePhoto/{id}")]
        public IActionResult DeletePhoto(int? id)
        {
            return Ok($"delete simulation {id}");
        }

        [HttpGet("/api/showGallery")]
        public IActionResult ShowGallery()
        {
            return Ok("showGallery simulation");
        }

        [HttpGet("/api/showPhoto/{id}")]
        public IActionResult ShowPhoto(int? id)
        {
            return Ok($"showPhoto simulation {id}");
        }

        [HttpGet("/api/showDetailedPhoto/{id}")]
        public IActionResult ShowDetailedPhoto(int? id)
        {
            return Ok($"showDetailedPhoto simulation {id}");
        }
    }
}
