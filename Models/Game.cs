namespace WebAPIProject.Models
{
    public class Games
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
        public Tournaments Tournament { get; set; }
    }
}
