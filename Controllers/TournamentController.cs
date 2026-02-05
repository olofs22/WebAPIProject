using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Models;
using WebAPIProject.Services;
using System.Linq;

namespace WebAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase //A controller based on the built in ControllerBase
    {
        private readonly TournamentsService _tournamentsService; //declaring service class
        public TournamentsController (TournamentsService tournamentsService) //declaring controller that uses tournaments service class
        {
           _tournamentsService = tournamentsService;
        }

        [HttpGet] //specify function for GET endpoint
        public ActionResult<IEnumerable<Tournaments>> Get([FromQuery] string? title) //funtion to get a list of tournaments by title or all if not specified
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var tournament = _tournamentsService.GetByTitle(title); //using service to find tournament with specified title
                if (tournament == null) return NotFound(); //404 code of not found
                return Ok(new[] { tournament });
            }
            return Ok(_tournamentsService.GetAll()); //200 code if found
        }

        [HttpGet("{int id}")] //specify function for GET(id) endpoint
        public ActionResult<Tournaments> Get(int id) //function to get a tournament through a specified id
        {
            var tournament = _tournamentsService.GetById(id); //goes through service to find tournament with specified id
            if (tournament == null) //                  <-                      
                return NotFound(); //404 code if not found  

            return Ok(tournament); //200 code if found
        }

        [HttpPost]

    }
}
