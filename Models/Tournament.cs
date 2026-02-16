namespace WebAPIProject.Models
{
    public class Tournaments
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxPlayers { get; set; }
        public DateTime StartDate { get; set; }
        public List<Game> Games { get; set; } = new();
    }
}
