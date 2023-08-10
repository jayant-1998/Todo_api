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
        public async Task<TodoItem> InsertTodoAsync(InsertRequestViewModel todoItem)
        {
            return await _repository.InsertTodoAsync(todoItem);
        }

        public async Task<TodoItem> DeleteTodoAsync(int id)
        {
            var response = await _repository.DeleteTodoAsync(id);
            if (response == null)
            {
                throw new Exception("id does not exists!!");
            }
            return response;
        }

        public async Task<TodoResponseViewModel> GetTodoByIdAsync(int id)
        {
            var response = await _repository.GetTodoByIdAsync(id);
            if (response == null)
            {
                throw new Exception("id does not exists!!");
            }
            return response;
        }
        public async Task<IEnumerable<TodoResponseViewModel>> GetAllCompletedTodoAsync()
        {
            return await _repository.GetAllCompletedTodoAsync();
        }

        public async Task<TodoResponseViewModel> CompleteTodoByIdAsync(int id)
        {
            var response = await _repository.CompleteTodoByIdAsync(id);
            if (response == null)
            {
                throw new Exception("id does not exists!!");
            }
            
            return response;
        }

        public async Task<IEnumerable<TodoResponseViewModel>> GetAllTodoAsync()
        {
            var response = await _repository.GetAllTodoAsync();
            if (response == null)
            {
                throw new Exception("no todo exists!!"); 
            }
            return response;
        }

        public async Task<TodoResponseViewModel> UpdateTodoAsync(int id, UpdateRequestViewModel todoItem)
        {
            var response = await _repository.UpdateTodoAsync(id, todoItem);

            if (response == null)
            {
                throw new Exception("id maybe does not exists or it already completed!!");
            }
            return response;
        }
    }
}
