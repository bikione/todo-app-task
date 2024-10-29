using System.Text.Json;
using System.Text.Json.Serialization;
using todoapp.App.Repository.Dtos;

namespace Todo_App_Task.App.Repository
{
    public interface IJsonToDoRepository
    {
        Task<IEnumerable<ToDo>> GetAllAsync();
        Task<ToDo?> GetByIdAsync(int id);
        Task AddAsync(ToDo todo);
        Task UpdateAsync(ToDo updatedToDo);
        Task DeleteAsync(int id);
    }
    public class JsonToDoRepository : IJsonToDoRepository
    {
        private readonly string _filePath = "ToDoData.json";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public JsonToDoRepository(string? filePath)
        {
            _filePath = filePath ?? _filePath;
        }

        public async Task<IEnumerable<ToDo>> GetAllAsync()
        {
            return await ReadFromFileAsync();
        }

        public async Task<ToDo?> GetByIdAsync(int id)
        {
            var todos = await ReadFromFileAsync();
            return todos.FirstOrDefault(t => t.Id == id);
        }

        public async Task AddAsync(ToDo todo)
        {
            var todos = (await ReadFromFileAsync()).ToList();
            todo.Id = todos.Any() ? todos.Max(t => t.Id) + 1 : 1;
            todos.Add(todo);
            await WriteToFileAsync(todos);
        }

        public async Task UpdateAsync(ToDo updatedToDo)
        {
            var todos = (await ReadFromFileAsync()).ToList();
            var index = todos.FindIndex(t => t.Id == updatedToDo.Id);
            if (index != -1)
            {
                todos[index] = updatedToDo;
                await WriteToFileAsync(todos);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var todos = (await ReadFromFileAsync()).ToList();
            var todoToDelete = todos.FirstOrDefault(t => t.Id == id);
            if (todoToDelete != null)
            {
                todos.Remove(todoToDelete);
                await WriteToFileAsync(todos);
            }
        }

        private async Task<IEnumerable<ToDo>> ReadFromFileAsync()
        {
            if (!File.Exists(_filePath)) return new List<ToDo>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<ToDo>>(json, _jsonOptions) ?? new List<ToDo>();
        }

        private async Task WriteToFileAsync(IEnumerable<ToDo> todos)
        {
            var json = JsonSerializer.Serialize(todos, _jsonOptions);
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
