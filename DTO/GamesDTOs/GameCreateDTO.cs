using WebAPIProject.Models;

namespace WebAPIProject.DTO.GamesDTOs
{
    public class GameCreateDTO //DTO controlling the data that can be set when creating the object
    {
        public string Title { get; set; } = null!;
        public int TournamentId { get; set; }
        public DateTime StartTime { get; set; } 
    }
}
