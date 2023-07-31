using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.DAL.DBContexts;
using TodoAPI.DAL.Entity;
using TodoAPI.DAL.Repositories.Interfaces;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.DAL.Repositories.Implementaations
{
    public class TodoRepositories : ITodoRepositories
    {
        private readonly ApplicationDBContext _dbContext;

        public TodoRepositories(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<ApiResponseViewModel>> CreateTodoItem(RequestModels todoItem)
        {
            try
            {
                TodoItem requestbody = new TodoItem
                {
                    ID = todoItem.Id,
                    Name = todoItem.Name,
                    Description = todoItem.Description,
                    CreatedAt = DateTime.Now,
                };

                _dbContext.Add(requestbody);
                await _dbContext.SaveChangesAsync();

                var reponse = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 200,
                    Message = "Succces",
                    Body = null
                };
                return reponse;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception or inspect it for more information
                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception message available";

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = "Error while saving changes",
                    Body = innerExceptionMessage
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return response;
            }
        }

        public async Task<ApiResponseViewModel> DeleteTodoDb(int id)
        {
            try
            {
                var body = await _dbContext.TodoItems
                    .Where(todo => todo.ID == id && !todo.IsDeleted)
                    .FirstOrDefaultAsync();

                body.IsDeleted = true;
                body.DeletedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 200,
                    Message = "Delete id succesfull",
                    Body = null
                };
            }
            catch (Exception ex) 
            {
                return new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
            }

        }

        public async Task<IEnumerable<ResponseTodoItem>> FetchAllDataAsync()
        {
            var body = await _dbContext.TodoItems
                            .Where(todo => !todo.IsCompleted && !todo.IsDeleted)
                            .ToListAsync(); 

            var responseTodoItems = body.Select(todo => new ResponseTodoItem
            {
                ID = todo.ID,
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                DeletedAt = todo.DeletedAt,
                UpdatedAt = todo.UpdatedAt,
                CreatedAt = todo.CreatedAt,
                IsDeleted = todo.IsDeleted,
                CompletedAt = todo.CompletedAt,
            }).ToList();

            return responseTodoItems;
        }

        public async Task<ResponseTodoItem> FetchAllDataByIdAsync(int id)
        {
            var body = _dbContext.TodoItems
                            .Where(todo => todo.ID == id && !todo.IsCompleted && !todo.IsDeleted);

            var responseTodoItems = await body.Select(todo => new ResponseTodoItem
            {
                ID = todo.ID,
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                DeletedAt = todo.DeletedAt,
                UpdatedAt = todo.UpdatedAt,
                CreatedAt = todo.CreatedAt,
                IsDeleted = todo.IsDeleted,
                CompletedAt = todo.CompletedAt,
            }).FirstOrDefaultAsync();

            return responseTodoItems;
        }

        public async Task<IEnumerable<ResponseTodoItem>> GetAllCompltedDb()
        {
            var body = _dbContext.TodoItems
                .Where(t => t.IsCompleted != false && !t.IsDeleted);

            var responseTodoItems = await body.Select(todo => new ResponseTodoItem
            {
                ID = todo.ID,
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                DeletedAt = todo.DeletedAt,
                UpdatedAt = todo.UpdatedAt,
                CreatedAt = todo.CreatedAt,
                IsDeleted = todo.IsDeleted,
                CompletedAt = todo.CompletedAt,
            }).ToListAsync();

            return responseTodoItems;
        }

        public async Task<ResponseTodoItem> GetCompletedByIdDb(int id)
        {
            var body = await _dbContext.TodoItems
                .Where (todo => todo.ID == id && !todo.IsCompleted && !todo.IsDeleted)
                .SingleOrDefaultAsync();
            if (body == null)
            {
                var responseerr = new ResponseTodoItem();
                
                return responseerr;
            }
            body.CompletedAt = DateTime.Now;
            body.IsCompleted = true;

            await _dbContext.SaveChangesAsync();

            var savechanges = new ResponseTodoItem
            {
                ID = body.ID,
                Name = body.Name,
                Description = body.Description,
                IsCompleted = body.IsCompleted,
                DeletedAt = body.DeletedAt,
                UpdatedAt = body.UpdatedAt,        
                CreatedAt = body.CreatedAt,
                IsDeleted = body.IsDeleted,
                CompletedAt = body.CompletedAt,

            };


            return savechanges;


        }

        public async Task<ApiResponseViewModel> UpdateTodoDb(int id, RequsetUpdateModels todoItem)
        {
            var body = await _dbContext.TodoItems
                .Where(todo => todo.ID == id && !todo.IsCompleted && !todo.IsDeleted)
                .SingleOrDefaultAsync();
            if (body == null)
            {
                var responseerr = new ApiResponseViewModel()
                {
                    Timestamp = DateTime.Now,
                    Message = "not found",
                    Code = 404,
                    Body = "erorr"

                };
                return responseerr;
            }
            body.Name = todoItem.Name;
            body.Description = todoItem.Description;
            body.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            var response = new ApiResponseViewModel()
            {
                Timestamp = DateTime.Now,
                Message = "Succes",
                Code = 200,
                Body = "update successfully"
            };
            return response;
        }
    }
}
