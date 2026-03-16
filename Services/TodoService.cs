using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPIProject.Data;
using WebAPIProject.Models;
using WebAPIProject.TodoDTOs;

namespace WebAPIProject.Services
{
    public class TodoService //service class that handles logic speaking to the database for Tournament objects, used by the TournamentController
    {
        private readonly AppDbContext _context; //dependency injection for database context

        public TodoService(AppDbContext context)//constructor for dependency injection
        {
            _context = context;
        }
        public async Task<List<TodoResponseDTO>> GetAll()
        {
            var todos = await _context.Todo.ToListAsync();
            return todos.Select(t => new TodoResponseDTO
            {
                Id = t.Id,
                Title = t.Text,
                Done = t.Done,
            }).ToList();
        }
        public async Task<TodoResponseDTO> GetById (int id) //function to get an object by id from the database
        {
            var todo = await _context.Todo
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null) return null;

            return new TodoResponseDTO //mapping to dto to control which information is sent to the client
            {
                Id = todo.Id,
                Title = todo.Text,
                Done = todo.Done,
            };
        }
        public async Task<TodoResponseDTO> Create(TodoCreateDTO tcdto) //function to create a new tournament object and add it to the database
        {
            var todo = new Todo  
            {
                Text = tcdto.Title,
                Done = tcdto.Done,
            };

            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();

            return await GetById(todo.Id);
        }
        public async Task<TodoResponseDTO> Update(int id, TodoUpdateDTO tudto) //function to edit or update existing tournament object in the database by id
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo == null) return null;
            todo.Done = tudto.Done ?? todo.Done; if (tudto.Title != null)
                todo.Text = tudto.Title;

            await _context.SaveChangesAsync();
            
            return await GetById(todo.Id);
        }
        public async Task<bool> Delete(int id) //function to delete an object from the database by id
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
                return false;

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
