using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.Services.Interface
{
    public interface ITodoServices
    {

        public ActionResult<ApiResponseViewModel> CreateTodo(RequestModels todoItem);
        public Task<IEnumerable<ResponseTodoItem>> TodoItems();
        public Task<ResponseTodoItem> FetchAllDataByIdAsync(int id);
        public Task<ApiResponseViewModel> Updatetodo(int id, RequsetUpdateModels todoItem);
        public Task<ApiResponseViewModel> DeleteTodo(int id);
        public Task<ResponseTodoItem> GetAllCompleted();
        public Task<ResponseTodoItem> GetCompletedById(int id);
    }
}
