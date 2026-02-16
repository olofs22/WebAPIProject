using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPIProject.Data;
using WebAPIProject.DTO.GamesDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class GamesService
    {
        private readonly AppDbContext _context;
        private readonly List<Games> _games = new();
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
        public Games? GetById(int id)
        {
            return _context.games.Find(id);
        }
    }
}
