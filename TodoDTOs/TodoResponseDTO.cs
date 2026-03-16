using WebAPIProject.Models;

namespace WebAPIProject.TodoDTOs
{
    public class TodoResponseDTO ////DTO controlling which data can be returned to the client
    {
        public int Id { get; set; }
        public string Title { get; set; } = null;
        public bool Done { get; set; }
    }
}
