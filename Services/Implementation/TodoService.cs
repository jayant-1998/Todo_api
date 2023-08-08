using TodoAPI.DAL.Entity;
using TodoAPI.DAL.Repositories.Interfaces;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;
using TodoAPI.Services.Interface;

namespace TodoAPI.Services.Implementation
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepositorie _repository;

        public TodoService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<ITodoRepositorie>();
        }
        public async Task<TodoItem> CreateTaskAsync(RequestModel todoItem)
        {
            return await _repository.CreateTaskAsync(todoItem);
        }

        public async Task<string> DeleteTaskAsync(int id)
        {
            return await _repository.DeleteTaskByIdDbAsync(id);
        }

        public async Task<ResponseTodoItem> GetTaskByIdAsync(int id)
        {
            return await _repository.GetTaskByIdDbAsync(id);
        }
        public async Task<IEnumerable<ResponseTodoItem>> GetAllCompleteTasksAsync()
        {
            return await _repository.GetAllCompleteTasksDbAsync();
        }

        public async Task<ResponseTodoItem> CompleteTaskByIdAsync(int id)
        {
            return await _repository.CompleteTaskByIdDbAsync(id);
        }

        public async Task<IEnumerable<ResponseTodoItem>> GetAllTasksAsync()
        {
            return await _repository.GetAllTasksFromDbAsync();
        }

        public async Task<ResponseTodoItem> UpdateTaskAsync(int id, RequsetUpdateModel todoItem)
        {
            return await _repository.UpdateTasksDbAsync(id, todoItem);
        }
    }
}
