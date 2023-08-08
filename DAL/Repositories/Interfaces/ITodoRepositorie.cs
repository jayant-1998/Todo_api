using Microsoft.AspNetCore.Mvc;
using TodoAPI.DAL.Entity;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.DAL.Repositories.Interfaces
{
    public interface ITodoRepositorie
    {
        public Task<IEnumerable<ResponseTodoItem>> GetAllTasksFromDbAsync();
        public Task<TodoItem> CreateTaskAsync(RequestModel todoItem);
        public Task<ResponseTodoItem> UpdateTasksDbAsync(int id, RequsetUpdateModel todoItem);
        public Task<ResponseTodoItem> GetTaskByIdDbAsync(int id);
        public Task<string> DeleteTaskByIdDbAsync(int id);
        public Task<IEnumerable<ResponseTodoItem>> GetAllCompleteTasksDbAsync();
        public Task<ResponseTodoItem> CompleteTaskByIdDbAsync(int id);
    }
}
