using WebAPIProject.Models;
using WebAPIProject.DTO.GamesDTOs;

namespace WebAPIProject.DTO.TournamentDTOs
{
    public class TournamentResponseDTO ////DTO controlling which data can be returned to the client
    {
        public int Id { get; set; }
        public string Title { get; set; } = null;
        public string Description { get; set; } = null;
        public int MaxPlayers { get; set; }
        //public DateTime StartDate { get; set; }
        public List<GameResponseDTO> Games { get; set; } = new List<GameResponseDTO>();
    }
}
