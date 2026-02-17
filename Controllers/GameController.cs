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
        public async Task<ActionResult<List<GameResponseDTO>>> GetAll(string? search = null)
        {
            var games = await _gamesService.GetAll(search);
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameResponseDTO>> GetById(int id)
        {
            var game = await _gamesService.GetById(id);

            if (game == null) return NotFound();

            return Ok(new GameResponseDTO
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId
            });
        }
        [HttpPost]
        public async Task<ActionResult<GameResponseDTO>> Create(GameCreateDTO gcdto)
        {
            var createdGame = await _gamesService.Create(gcdto);
            return CreatedAtAction(nameof(GetAll), new { id = createdGame.Id }, createdGame);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GameResponseDTO>> Update(int id, GameUpdateDTO gudto)
        {
            var updatedGame = await _gamesService.Update(id, gudto);
            if (updatedGame == null)
                return NotFound();  
            return Ok(updatedGame);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _gamesService.Delete(id))
                return NotFound();
            return NoContent();
        }
    }
}
