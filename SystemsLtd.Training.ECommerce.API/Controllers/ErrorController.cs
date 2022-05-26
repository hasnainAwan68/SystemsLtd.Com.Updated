using Microsoft.AspNetCore.Mvc;
using SystemsLtd.Training.ECommerce.API.Models;

namespace SystemsLtd.Training.ECommerce.API.Controllers
{
    public class ErrorController : Controller
    {

        [Route("Error/{statusCode}")]
        [HttpGet]
        public IActionResult HttpStatusCodeHandler(int statusCode)

        {
            var Message = new ErrorMessage();
            switch (statusCode)
            {
                case 404:
                    Message.Message = "Sorry, The Resource you Requested Couldn't be found";
                    break;
            }
            return Ok(Message);
            //return View("NotFound", Message);
        }
    }
}
