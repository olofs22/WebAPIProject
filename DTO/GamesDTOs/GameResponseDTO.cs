using Microsoft.EntityFrameworkCore;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.DTO.GamesDTOs
{
    public class GameResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
        public Tournaments Tournament { get; set; } = null!;
    }
}
