using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIProject.DTO.GamesDTOs;
using WebAPIProject.DTO.TournamentDTOs;
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

        [HttpGet("{id:int}")] //specify function for GET(id) endpoint
        public ActionResult<GameResponseDTO> GetById(int id) //function to get a tournament object through a specified id
        {
            var game = _gamesService.GetById(id); //goes through service to find tournament with specified id
           
            return Ok(new GameResponseDTO  
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId
            });
        }

        [HttpPost]
        public ActionResult<GameResponseDTO> Create(GameCreateDTO tcdto)
        {
            var createdGame = _gamesService.Create(tcdto);
            return CreatedAtAction(nameof(GetAll), new { id = createdGame.Id }, createdGame);
        }

        [HttpPut("{id:int}")]
        public ActionResult<GameResponseDTO> Update(int id, GameUpdateDTO gudto)
        {
            var updatedGame = _gamesService.Update(id, gudto);
            if (updatedGame == null)
                return NotFound();
            return Ok(updatedGame);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (!_gamesService.Delete(id))
                return NotFound();
            return NoContent();
        }
    }
}
