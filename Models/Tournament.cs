namespace WebAPIProject.Models
{
    public class Tournaments
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Maxplayers { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Games> Games { get; set; }
    }
}
