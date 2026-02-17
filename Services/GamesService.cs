using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPIProject.Data;
using WebAPIProject.DTO.GamesDTOs;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class GamesService
    {
        private readonly AppDbContext _context;
        private readonly List<Game> _games = new();
        private int _nextId = 1;

        public GamesService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<GameResponseDTO>> GetAll(string? search = null)
        {
            var query = _context.Games
                .Include(g => g.Tournament)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            var games = await query.ToListAsync();

            return games.Select(t => new GameResponseDTO
            {
                Id = t.Id,
                Title = t.Title,
                Time = t.Time,
                TournamentId = t.TournamentId,
                Tournament = t.Tournament == null ? null : new TournamentResponseDTO 
                {
                    Id = t.Tournament.Id,
                    Title = t.Tournament.Title,
                    Description = t.Tournament.Description,
                    MaxPlayers = t.Tournament.MaxPlayers,
                    StartDate = t.Tournament.StartDate
                }
            }).ToList();
        }
        public async Task<GameResponseDTO> GetById(int id)
        {
            var game = await _context.Games
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return null;

            return new GameResponseDTO
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId,
                Tournament = game.Tournament == null ? null : new TournamentResponseDTO
                {
                    Id = game.Tournament.Id,
                    Title = game.Tournament.Title,
                    Description = game.Tournament.Description,
                    MaxPlayers = game.Tournament.MaxPlayers,
                    StartDate = game.Tournament.StartDate
                }   
            };
        }
        public async Task<GameResponseDTO> Create(GameCreateDTO gcdto)
        {
            var game = new Game
            {
                Title = gcdto.Title,
                
                Time = gcdto.StartTime,

                TournamentId = gcdto.TournamentId
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return await GetById(game.Id);
        }

        public async Task<GameResponseDTO?> Update(int id, GameUpdateDTO gudto)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return null;

            if (gudto.Title != null)
                game.Title = gudto.Title;
            if (gudto.Time.HasValue)
                game.Time = gudto.Time.Value;

            _context.SaveChangesAsync();

           return await GetById(game.Id);
        }
        public async Task<bool> Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return false;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
