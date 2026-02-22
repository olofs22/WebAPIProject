namespace WebAPIProject.Models
{
    public class Game //object for Game model
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!; 
    }
}
