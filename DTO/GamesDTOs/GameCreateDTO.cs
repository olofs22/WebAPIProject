using WebAPIProject.Models;

namespace WebAPIProject.DTO.GamesDTOs
{
    public class GameCreateDTO
    {
        public string Title { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public int TournamentId { get; set; }

    }
}
