using TodoAPI.DAL.Entity;
using TodoAPI.DAL.Repositories.Interfaces;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;
using TodoAPI.Services.Interface;

namespace TodoAPI.Services.Implementation
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<ITodoRepository>();
        }
        public async Task<TodoItem> InsertTaskAsync(InsertRequestViewModel todoItem)
        {
            return await _repository.InsertTaskAsync(todoItem);
        }

        public async Task<string> DeleteTaskAsync(int id)
        {
            return await _repository.DeleteTaskAsync(id);
        }

        public async Task<TaskResponseViewModel> GetTaskByIdAsync(int id)
        {
            return await _repository.GetTaskByIdAsync(id);
        }
        public async Task<IEnumerable<TaskResponseViewModel>> GetAllCompleteTasksAsync()
        {
            return await _repository.GetAllCompleteTasksAsync();
        }

        public async Task<TaskResponseViewModel> CompletedTaskAsync(int id)
        {
            return await _repository.CompleteTaskAsync(id);
        }

        public async Task<IEnumerable<TaskResponseViewModel>> GetAllTasksAsync()
        {
            return await _repository.GetAllTasksAsync();
        }

        public async Task<TaskResponseViewModel> UpdateTaskAsync(int id, UpdateRequestViewModel todoItem)
        {
            return await _repository.UpdateTaskAsync(id, todoItem);
        }
    }
}
