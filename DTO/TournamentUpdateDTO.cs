namespace WebAPIProject.DTO
{
    public class TournamentUpdateDTO
    {
            public string? Title { get; set; }        
            public string? Description { get; set; }  
            public int? MaxPlayers { get; set; }      
            public DateTime? StartDate { get; set; }  
    }
}
