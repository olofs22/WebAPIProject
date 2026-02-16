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
        public List<GameResponseDTO> GetAll(string? search = null)
        {
            var query = _context.games.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            var games = query.ToList();

            return games.Select(t => new GameResponseDTO
            {
                Id = t.Id,
                Title = t.Title,
                Time = t.Time,
                TournamentId = t.TournamentId
            }).ToList();
        }
        public GameResponseDTO GetById(int id)
        {
            var game = _context.games
                .Include(g => g.Tournament)  
                .FirstOrDefault(g => g.Id == id);

            return new GameResponseDTO
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId,
                Tournament = game.Tournament  
            };
        }
        public GameResponseDTO Create(GameCreateDTO gcdto)
        {
            var game = new Game
            {
                Title = gcdto.Title,
                
                Time = gcdto.StartTime,

                TournamentId = gcdto.TournamentId
            };

            _context.games.Add(game);
            _context.SaveChanges();

            return new GameResponseDTO  
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId
            };
        }

        public GameResponseDTO? Update(int id, GameUpdateDTO gudto)
        {
            var game = _context.games.Find(id);
            if (game == null) return null;

            if (gudto.Title != null)
                game.Title = gudto.Title;
            if (gudto.Time.HasValue)
                game.Time = gudto.Time.Value;

            _context.SaveChanges();

            return new GameResponseDTO
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId
            };
        }
        public bool Delete(int id)
        {
            var game = _context.games.Find(id);
            if (game == null)
                return false;

            _context.games.Remove(game);
            _context.SaveChanges();
            return true;
        }


    }
}
