using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPIProject.Data;
using WebAPIProject.DTO;
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
        public IEnumerable<Tournaments> GetAll()
        {
            return _context.tournament.ToList();
        }
        public Tournaments? GetById (int id)
        {
            return _context.tournament.Find(id);
        }
        public Tournaments? GetByTitle(string title)
        {
            return _context.tournament.FirstOrDefault(i => i.Title == title);
        }
        public TournamentResponseDTO Create(TournamentCreateDTO tcdto)
        {
            var tournament = new Tournaments  // ← Mappa DTO → entity
            {
                Title = tcdto.Title,
                Description = tcdto.Description,
                MaxPlayers = tcdto.MaxPlayers,
                StartDate = tcdto.StartDate
            };

            _context.tournament.Add(tournament);
            _context.SaveChanges();

            return new TournamentResponseDTO  // ← Mappa entity → ResponseDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                Description = tournament.Description,
                MaxPlayers = tournament.MaxPlayers,
                StartDate = tournament.StartDate
            };
        }
        public TournamentResponseDTO? Update(int id, TournamentUpdateDTO tudto)
        {
            var tournament = _context.tournament.Find(id);
            if (tournament == null) return null;

            if (tudto.Title != null)
                tournament.Title = tudto.Title;

            if (tudto.Description != null)
                tournament.Description = tudto.Description;

            if (tudto.MaxPlayers.HasValue)
                tournament.MaxPlayers = tudto.MaxPlayers.Value;

            if (tudto.StartDate.HasValue)
                tournament.StartDate = tudto.StartDate.Value;

            _context.SaveChanges();

            // Returnera ResponseDTO
            return new TournamentResponseDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                Description = tournament.Description,
                MaxPlayers = tournament.MaxPlayers,
                StartDate = tournament.StartDate
            };
        }
        public bool Delete(int id)
        {
            var tournament = _context.tournament.Find(id);
            if (tournament == null)
                return false;

            _context.tournament.Remove(tournament);
            _context.SaveChanges();
            return true;
        }
    }
}
