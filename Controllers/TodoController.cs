using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIProject.Models;
using WebAPIProject.Services;
using WebAPIProject.TodoDTOs;

namespace WebAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase 
    {
        private readonly TodoService _todoService; //declaring service for dependency injection
        public TodoController (TodoService todoService) //constructor for dependency injection
        {
            _todoService = todoService;
        }

        [HttpGet] //endpoint for GetAll and search by title 
        public async Task<ActionResult<List<TodoResponseDTO>>> GetAll() 
        {
            var todo = await _todoService.GetAll();
            return Ok(todo);
        }

        [HttpGet("{id:int}")] //endpoint for GetById
        public async Task<ActionResult<TodoResponseDTO>> GetById(int id)
        {
            var todo = await _todoService.GetById(id); 
            return Ok(todo); 
        }

        [HttpPost] //endpoint for creating a tournament object
        public async Task<ActionResult<TodoResponseDTO>> Create(TodoCreateDTO tcdto)
        {
            var createdTodo = await _todoService.Create(tcdto); 
            return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id:int}")] //endpoint for editing a tournament object by id
        public async Task<ActionResult<TodoResponseDTO>> Update(int id, TodoUpdateDTO tudto)
        {
            var updatedTodo = await _todoService.Update(id, tudto);

            if (updatedTodo == null)  return NotFound();

            return Ok(updatedTodo);
        }

        [HttpDelete("{id:int}")] //endpoint for deleting a tournament object by id
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _todoService.Delete(id))
                return NotFound();
            return NoContent();
        }
    }
}
