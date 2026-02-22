using WebAPIProject.Models;

namespace WebAPIProject.DTO.GamesDTOs
{
    public class GameUpdateDTO //controlling which data can be edited from existing objects from the client
    {
        public string? Title { get; set; }
        public DateTime? Time { get; set; }
    }
}
