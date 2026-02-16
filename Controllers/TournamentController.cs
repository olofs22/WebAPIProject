using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;
using WebAPIProject.Services;

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
        public ActionResult<List<TournamentResponseDTO>> GetAll(string? search = null) //funtion to get a list of tournament objects by title or all if not specified
        {
            var tournaments = _tournamentsService.GetAll(search);
            return Ok(tournaments);
        }

        [HttpGet("{id:int}")] //specify function for GET(id) endpoint
        public ActionResult<TournamentResponseDTO> GetById(int id) //function to get a tournament object through a specified id
        {
            var tournament = _tournamentsService.GetById(id); //goes through service to find tournament with specified id
            return Ok(tournament); //200 code if found
        }

        [HttpPost]
        public ActionResult<TournamentResponseDTO> Create(TournamentCreateDTO tcdto)
        {
            var createdTournament = _tournamentsService.Create(tcdto);  // ← Fungerar nu!
            return CreatedAtAction(nameof(GetAll), new { id = createdTournament.Id }, createdTournament);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TournamentResponseDTO> Update(int id, TournamentUpdateDTO tudto)
        {
            var updatedTournament = _tournamentsService.Update(id, tudto);
            if (updatedTournament == null)
                return NotFound();
            return Ok(updatedTournament);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (!_tournamentsService.Delete(id))
                return NotFound();
            return NoContent();
        }
    }
}
