using Microsoft.AspNetCore.Mvc;
using TodoAPI.DAL.Entity;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.DAL.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        public Task<IEnumerable<TodoResponseViewModel>> GetAllTodoAsync();
        public Task<TodoItem> InsertTodoAsync(InsertRequestViewModel todoItem);
        public Task<TodoResponseViewModel> UpdateTodoAsync(int id, UpdateRequestViewModel todoItem);
        public Task<TodoResponseViewModel> GetTodoByIdAsync(int id);
        public Task<TodoItem> DeleteTodoAsync(int id);
        public Task<IEnumerable<TodoResponseViewModel>> GetAllCompletedTodoAsync();
        public Task<TodoResponseViewModel> CompleteTodoByIdAsync(int id);
    }
}
