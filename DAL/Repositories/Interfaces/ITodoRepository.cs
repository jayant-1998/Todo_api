using Microsoft.AspNetCore.Mvc;
using TodoAPI.DAL.Entity;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.DAL.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        public Task<IEnumerable<TaskResponseViewModel>> GetAllTasksAsync();
        public Task<TodoItem> InsertTaskAsync(InsertRequestViewModel todoItem);
        public Task<TaskResponseViewModel> UpdateTaskAsync(int id, UpdateRequestViewModel todoItem);
        public Task<TaskResponseViewModel> GetTaskByIdAsync(int id);
        public Task<string> DeleteTaskAsync(int id);
        public Task<IEnumerable<TaskResponseViewModel>> GetAllCompleteTasksAsync();
        public Task<TaskResponseViewModel> CompleteTaskAsync(int id);
    }
}
