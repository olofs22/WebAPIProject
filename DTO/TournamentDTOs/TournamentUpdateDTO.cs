using WebAPIProject.Models;

namespace WebAPIProject.DTO.TournamentDTOs
{
    public class TournamentUpdateDTO //controlling which data can be edited from existing objects from the client
    {
            public string? Title { get; set; }        
            public string? Description { get; set; }  
            public int? MaxPlayers { get; set; }
            public DateTime? StartDate { get; set; }
    }
}
