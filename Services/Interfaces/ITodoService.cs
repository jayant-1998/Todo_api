using TodoAPI.DAL.Entity;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.Services.Interface
{
    public interface ITodoService
    {
        public Task<TodoItem> InsertTodoAsync(InsertRequestViewModel todoItem);
        public Task<IEnumerable<GetAllTodoResponseViewModel>> GetAllTodoAsync();
        public Task<TodoResponseViewModel> GetTodoByIdAsync(int id);
        public Task<TodoResponseViewModel> UpdateTodoAsync(int id, UpdateRequestViewModel todoItem);
        public Task<TodoItem> DeleteTodoAsync(int id);
        public Task<IEnumerable<GetAllTodoResponseViewModel>> GetAllCompletedTodoAsync();
        public Task<TodoResponseViewModel> CompleteTodoByIdAsync(int id);
    }
}
