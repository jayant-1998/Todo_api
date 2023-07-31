using Microsoft.AspNetCore.Mvc;
using TodoAPI.DAL.Repositories.Interfaces;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;
using TodoAPI.Services.Interface;

namespace TodoAPI.Services.Implementation
{
    public class TodoServices : ITodoServices
    {
        private readonly ITodoRepositories _repository;

        public TodoServices(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<ITodoRepositories>();

        }
        public async Task<ActionResult<ApiResponseViewModel>> CreateTodo(RequestModels todoItem)
        {
            return await _repository.CreateTodoItem(todoItem);
            
        }

        public async Task<ApiResponseViewModel> DeleteTodo(int id)
        {
            return await _repository.DeleteTodoDb(id);
            ;
        }

        public async Task<ResponseTodoItem> FetchAllDataByIdAsync(int id)
        {
            var data = await _repository.FetchAllDataByIdAsync(id);
            if (data == null)
            {
                return new ResponseTodoItem();
            }
            return data;

        }

        public async Task<IEnumerable<ResponseTodoItem>> GetAllCompleted()
        {
            return await _repository.GetAllCompltedDb();
        }

        public async Task<ResponseTodoItem> GetCompletedById(int id)
        {
            return await _repository.GetCompletedByIdDb(id);

        }

        public async Task<IEnumerable<ResponseTodoItem>> FetchAllDataAsync()
        {
            return await _repository.FetchAllDataAsync();
        }

        public async Task<ApiResponseViewModel> Updatetodo(int id, RequsetUpdateModels todoItem)
        {
            return await _repository.UpdateTodoDb(id, todoItem);
            ;
        }
    }
}
