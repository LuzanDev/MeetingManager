using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManager.API.Controllers
{
    [ApiController]
    public class RoomsController : ControllerBase
    {

        [HttpGet]
        public ActionResult Index()
        {
            
        }

        

        // POST: RoomsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            
        }
    }
}
