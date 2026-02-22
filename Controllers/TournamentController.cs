using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;
using WebAPIProject.Services;

namespace WebAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase 
    {
        private readonly TournamentsService _tournamentsService; //declaring service for dependency injection
        public TournamentsController (TournamentsService tournamentsService) //constructor for dependency injection
        {
           _tournamentsService = tournamentsService;
        }

        [HttpGet] //endpoint for GetAll and search by title 
        public async Task<ActionResult<List<TournamentResponseDTO>>> GetAll(string? search = null) 
        {
            var tournaments = await _tournamentsService.GetAll(search);
            return Ok(tournaments);
        }

        [HttpGet("{id:int}")] //endpoint for GetById
        public async Task<ActionResult<TournamentResponseDTO>> GetById(int id)
        {
            var tournament = await _tournamentsService.GetById(id); 
            return Ok(tournament); 
        }

        [HttpPost] //endpoint for creating a tournament object
        public async Task<ActionResult<TournamentResponseDTO>> Create(TournamentCreateDTO tcdto)
        {
            var createdTournament = await _tournamentsService.Create(tcdto); 
            return CreatedAtAction(nameof(GetById), new { id = createdTournament.Id }, createdTournament);
        }

        [HttpPut("{id:int}")] //endpoint for editing a tournament object by id
        public async Task<ActionResult<TournamentResponseDTO>> Update(int id, TournamentUpdateDTO tudto)
        {
            var updatedTournament = await _tournamentsService.Update(id, tudto);

            if (updatedTournament == null)  return NotFound();

            return Ok(updatedTournament);
        }

        [HttpDelete("{id:int}")] //endpoint for deleting a tournament object by id
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _tournamentsService.Delete(id))
                return NotFound();
            return NoContent();
        }
    }
}
