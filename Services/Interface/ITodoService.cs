using TodoAPI.DAL.Entity;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.Services.Interface
{
    public interface ITodoService
    {
        public Task<TodoItem> CreateTaskAsync(RequestModel todoItem);
        public Task<IEnumerable<ResponseTodoItem>> GetAllTasksAsync();
        public Task<ResponseTodoItem> GetTaskByIdAsync(int id);
        public Task<ResponseTodoItem> UpdateTaskAsync(int id, RequsetUpdateModel todoItem);
        public Task<string> DeleteTaskAsync(int id);
        public Task<IEnumerable<ResponseTodoItem>> GetAllCompleteTasksAsync();
        public Task<ResponseTodoItem> CompleteTaskByIdAsync(int id);
    }
}
