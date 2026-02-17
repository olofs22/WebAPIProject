using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPIProject.Data;
using WebAPIProject.DTO.GamesDTOs;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class TournamentsService
    {
        private readonly AppDbContext _context;
        private readonly List<Tournaments> _tournaments = new();
        private int _nextId = 1;

        public TournamentsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<TournamentResponseDTO>> GetAll(string? search = null)
        {
            var query = _context.Tournaments
                .Include(t => t.Games)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            var tournaments = await query.ToListAsync();

            return tournaments.Select(t => new TournamentResponseDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                MaxPlayers = t.MaxPlayers,
                StartDate = t.StartDate,
                Games = t.Games.Select(g => new GameResponseDTO
                {
                    Id = g.Id,
                    Title = g.Title,
                    Time = g.Time,
                    TournamentId = g.TournamentId,
                    Tournament = null 
                }).ToList()
            }).ToList();
        }
        public async Task<TournamentResponseDTO> GetById (int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null) return null;

            return new TournamentResponseDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                Description = tournament.Description,
                MaxPlayers = tournament.MaxPlayers,
                StartDate = tournament.StartDate,
                Games = tournament.Games.Select(g => new GameResponseDTO
                {
                    Id = g.Id,
                    Title = g.Title,
                    Time = g.Time,
                    TournamentId = g.TournamentId,
                    Tournament = null 
                }).ToList()
            };
        }
        public async Task<TournamentResponseDTO> GetByTitle(string title)
        {
            var tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Title == title);
            if (tournament == null) return null;

            return new TournamentResponseDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                Description = tournament.Description,
                MaxPlayers = tournament.MaxPlayers,
                StartDate = tournament.StartDate
            };
        }
        public async Task<TournamentResponseDTO> Create(TournamentCreateDTO tcdto)
        {
            var tournament = new Tournaments  
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
        public async Task<TournamentResponseDTO> Update(int id, TournamentUpdateDTO tudto)
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
        public async Task<bool> Delete(int id)
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
