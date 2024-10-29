using Microsoft.AspNetCore.Mvc;
using Todo_App_Task.App.Repository;
using todoapp.App.Repository.Dtos;

namespace Todo_App_Task.App.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IJsonToDoRepository _repository;

        public ToDoController(IJsonToDoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDos()
        {
            var todos = await _repository.GetAllAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDo(int id)
        {
            var todo = await _repository.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo todo)
        {
            await _repository.AddAsync(todo);
            return CreatedAtAction(nameof(GetToDo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, ToDo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            
            var todoExist = await _repository.GetByIdAsync(id);
            if (todoExist == null) return NotFound();

            await _repository.UpdateAsync(todo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) return NotFound();
            
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}