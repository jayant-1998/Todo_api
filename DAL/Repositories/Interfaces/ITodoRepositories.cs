using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.DAL.Repositories.Interfaces
{
    public interface ITodoRepositories
    {
        public Task<IEnumerable<ResponseTodoItem>> FetchAllDataAsync();
        public Task<ActionResult<ApiResponseViewModel>> CreateTodoItem(RequestModels todoItem);
        public Task<ApiResponseViewModel> UpdateTodoDb(int id, RequsetUpdateModels todoItem);
        public Task<ResponseTodoItem> FetchAllDataByIdAsync(int id);
        public Task<ApiResponseViewModel> DeleteTodoDb(int id);
        public Task<IEnumerable<ResponseTodoItem>> GetAllCompltedDb();
        public Task<ResponseTodoItem> GetCompletedByIdDb(int id);
    }
}
