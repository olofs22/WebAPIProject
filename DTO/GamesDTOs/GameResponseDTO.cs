using Microsoft.EntityFrameworkCore;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.DTO.GamesDTOs
{
    public class GameResponseDTO //DTO controlling which data can be returned to the client
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
        public TournamentResponseDTO? Tournament { get; set; } = null!;
    }
}
