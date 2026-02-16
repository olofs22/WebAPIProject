using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIProject.DTO.GamesDTOs;
using WebAPIProject.Models;
using WebAPIProject.Services;

namespace WebAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly GamesService _gamesService;

        public GamesController (GamesService gamesService)
        {
            _gamesService = gamesService;
        }

        [HttpGet]
        public ActionResult<List<GameResponseDTO>> GetAll(string? search = null)
        {
            var games = _gamesService.GetAll(search);
            return Ok(games);
        }
    }
}
