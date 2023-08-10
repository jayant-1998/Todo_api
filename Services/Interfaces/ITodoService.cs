using TodoAPI.DAL.Entity;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.Services.Interface
{
    public interface ITodoService
    {
        public Task<TodoItem> InsertTaskAsync(InsertRequestViewModel todoItem);
        public Task<IEnumerable<TaskResponseViewModel>> GetAllTasksAsync();
        public Task<TaskResponseViewModel> GetTaskByIdAsync(int id);
        public Task<TaskResponseViewModel> UpdateTaskAsync(int id, UpdateRequestViewModel todoItem);
        public Task<string> DeleteTaskAsync(int id);
        public Task<IEnumerable<TaskResponseViewModel>> GetAllCompleteTasksAsync();
        public Task<TaskResponseViewModel> CompletedTaskAsync(int id);
    }
}
