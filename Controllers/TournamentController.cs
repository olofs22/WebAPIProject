using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Models;
using WebAPIProject.Services;
using System.Linq;

namespace WebAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentsService _tournamentsService;

        public TournamentsController (TournamentsService tournamentsService)
        {
           _tournamentsService = tournamentsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tournaments>> Get([FromQuery] string? title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var tournament = _tournamentsService.GetByTitle(title);
                if (tournament == null) return NotFound();
                return Ok(new[] { tournament });
            }
            return Ok(_tournamentsService.GetAll());
        }
    }
}
