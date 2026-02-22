using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPIProject.Data;
using WebAPIProject.DTO.GamesDTOs;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class TournamentsService //service class that handles logic speaking to the database for Tournament objects, used by the TournamentController
    {
        private readonly AppDbContext _context; //dependency injection for database context

        public TournamentsService(AppDbContext context)//constructor for dependency injection
        {
            _context = context;
        }
        public async Task<List<TournamentResponseDTO>> GetAll(string? search = null) //function to get all objects from database or search by title
        {
            var query = _context.Tournaments
                .Include(t => t.Games)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            var tournaments = await query.ToListAsync();

            return tournaments.Select(t => new TournamentResponseDTO //mapping through dto to only send necessary information to the client 
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                MaxPlayers = t.MaxPlayers,
                StartDate = t.StartDate,
                Games = t.Games.Select(g => new GameResponseDTO //mapping through game dto to get which tournament the game object belongs to
                {
                    Id = g.Id,
                    Title = g.Title,
                    Time = g.Time,
                    TournamentId = g.TournamentId,
                    Tournament = null 
                }).ToList()
            }).ToList();
        }
        public async Task<TournamentResponseDTO> GetById (int id) //function to get an object by id from the database
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null) return null;

            return new TournamentResponseDTO //mapping to dto to control which information is sent to the client
            {
                Id = tournament.Id,
                Title = tournament.Title,
                Description = tournament.Description,
                MaxPlayers = tournament.MaxPlayers,
                StartDate = tournament.StartDate,
                Games = tournament.Games.Select(g => new GameResponseDTO //mapping through game dto to get which games belong to that tournament
                {
                    Id = g.Id,
                    Title = g.Title,
                    Time = g.Time,
                    TournamentId = g.TournamentId,
                    Tournament = null 
                }).ToList()
            };
        }
        public async Task<TournamentResponseDTO> Create(TournamentCreateDTO tcdto) //function to create a new tournament object and add it to the database
        {
            var tournament = new Tournament  
            {
                Title = tcdto.Title,
                Description = tcdto.Description,
                MaxPlayers = tcdto.MaxPlayers,
                StartDate = tcdto.StartDate
            };

            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return await GetById(tournament.Id);
        }
        public async Task<TournamentResponseDTO> Update(int id, TournamentUpdateDTO tudto) //function to edit or update existing tournament object in the database by id
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null) return null;

            if (tudto.Title != null)
                tournament.Title = tudto.Title;

            if (tudto.Description != null)
                tournament.Description = tudto.Description;

            if (tudto.MaxPlayers.HasValue)
                tournament.MaxPlayers = tudto.MaxPlayers.Value;

            if (tudto.StartDate.HasValue)
                tournament.StartDate = tudto.StartDate.Value;

            await _context.SaveChangesAsync();
            
            return await GetById(tournament.Id);
        }
        public async Task<bool> Delete(int id) //function to delete an object from the database by id
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
                return false;

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
