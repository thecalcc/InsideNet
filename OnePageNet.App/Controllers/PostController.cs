using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Models.PostDTOs;

namespace OnePageNet.App.Controllers
{
    public class PostController : Controller
    {
        // TODO

        [HttpGet]
        public ActionResult Details([FromRoute] string id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreatePostDTO createPostDto)
        {
            //TODO Business Logic
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit([FromRoute] string id)
        {
            //TODO Business Logic
            return View();
        }

        [HttpPost] // TODO fix this - we have to make a put request for edit, if not whichever property we submit with null value will update with null value
        public ActionResult Edit([FromBody] EditPostDTO editPostDTO)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete([FromRoute] string id)
        {            
            //TODO Business Logic
            return View();
        }

        [HttpPost]
        public ActionResult DeletePost(string id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
}
