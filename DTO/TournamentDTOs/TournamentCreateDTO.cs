using WebAPIProject.Models;

namespace WebAPIProject.DTO.TournamentDTOs
{
    public class TournamentCreateDTO
    {
        public string Title { get; set; } = null;
        public string Description { get; set; }
        public int MaxPlayers { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Games> Games { get; set; }
    }
}
