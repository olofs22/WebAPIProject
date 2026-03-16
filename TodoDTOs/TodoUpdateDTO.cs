using WebAPIProject.Models;

namespace WebAPIProject.TodoDTOs
{
    public class TodoUpdateDTO //controlling which data can be edited from existing objects from the client
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public bool? Done {get; set;}
    }
}
