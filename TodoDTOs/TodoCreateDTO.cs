using WebAPIProject.Models;

namespace WebAPIProject.TodoDTOs
{
    public class TodoCreateDTO //DTO controlling the data that can be set when creating the object
    {
        public int Id { get; set; }
        public string Title { get; set; } = null;
        public bool Done { get; set; }
    }
}
