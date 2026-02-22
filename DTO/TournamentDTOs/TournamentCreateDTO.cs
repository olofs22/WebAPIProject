using WebAPIProject.Models;

namespace WebAPIProject.DTO.TournamentDTOs
{
    public class TournamentCreateDTO //DTO controlling the data that can be set when creating the object
    {
        public string Title { get; set; } = null;
        public string Description { get; set; }
        public int MaxPlayers { get; set; }

        public DateTime StartDate { get; set; }
    }
}
