using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPIProject.Data;
using WebAPIProject.DTO.GamesDTOs;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class GamesService //service class that handles logic speaking to the database for Game objects, used by the GameController
    {
        private readonly AppDbContext _context; //dependency injection for database context
        public GamesService(AppDbContext context) //constructor for dependency injection
        {
            _context = context;
        }
        public async Task<List<GameResponseDTO>> GetAll(string? search = null) //function to get all objects from database or search by title
        {
            var query = _context.Games
                .Include(g => g.Tournament)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            var games = await query.ToListAsync();

            return games.Select(t => new GameResponseDTO //mapping through dto to only send necessary information to the client
            {
                Id = t.Id,
                Title = t.Title,
                //Time = t.Time,
                TournamentId = t.TournamentId,
                Tournament = t.Tournament == null ? null : new TournamentResponseDTO //mapping through tournament dto to get which tournament the game object belongs to
                {
                    Id = t.Tournament.Id,
                    Title = t.Tournament.Title,
                    Description = t.Tournament.Description,
                    MaxPlayers = t.Tournament.MaxPlayers,
                    //StartDate = t.Tournament.StartDate
                }
            }).ToList();
        }
        public async Task<GameResponseDTO> GetById(int id) //function to get an object by id from the database
        {
            var game = await _context.Games
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return null;

            return new GameResponseDTO //mapping to dto to control which information is sent to the client
            {
                Id = game.Id,
                Title = game.Title,
              //  Time = game.Time,
                TournamentId = game.TournamentId,
                Tournament = game.Tournament == null ? null : new TournamentResponseDTO //mapping through tournament dto to get which tournament the game object belongs to
                {
                    Id = game.Tournament.Id,
                    Title = game.Tournament.Title,
                    Description = game.Tournament.Description,
                    MaxPlayers = game.Tournament.MaxPlayers,
                  //  StartDate = game.Tournament.StartDate
                }   
            };
        }
        public async Task<GameResponseDTO> Create(GameCreateDTO gcdto) //function to create a new game object and add it to the database
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

        public async Task<GameResponseDTO?> Update(int id, GameUpdateDTO gudto) //function to edit or update existing game object in the database by id
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return null;

            if (gudto.Title != null) 
                game.Title = gudto.Title;
            if (gudto.Time.HasValue)
                game.Time = gudto.Time.Value;

            await _context.SaveChangesAsync();

           return await GetById(game.Id);
        }
        public async Task<bool> Delete(int id) //function to delete an object from the database by id
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
