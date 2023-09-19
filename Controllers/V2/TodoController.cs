using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;
using TodoAPI.Services.Implementation;
using TodoAPI.Services.Interface;

namespace TodoAPI.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]

    public class TodoController : ControllerBase
    {
        //private readonly ITodoService _service;
        private readonly ITodoService todoService;

        public TodoController(IServiceProvider service)
        {
            //_service = service.GetRequiredService<ITodoService>();
            todoService = (ITodoService)service.GetRequiredService(typeof(TodoServiceV2)); //this same interface for multiple services
        }

        // GET: show all todo
        [Route("get-all-todo")]
        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
            try
            {
                IEnumerable<GetAllTodoResponseViewModel> result = await todoService.GetAllTodoAsync();
                //IEnumerable<GetAllTodoResponseViewModel> result1 = await todoService.GetAllTodoAsync();


                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "success",
                    Body = result
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
                return Ok(response);
            }
        }

        // GET: show todo by id
        [HttpGet("get-todo/{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            try
            {
                TodoResponseViewModel result = await todoService.GetTodoByIdAsync(id);

                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "success",
                    Body = result
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
                return Ok(response);
            }
        }

        // POST: insert data
        [Route("insert-todo")]
        [HttpPost]
        public async Task<IActionResult> InsertTodo(InsertRequestViewModel todoItem)
        {
            try
            {
                var result = await todoService.InsertTodoAsync(todoItem);

                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "Success",
                    Body = result
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }

        // PUT: update todo by id 
        [HttpPut("update-todo/{id}")]
        public async Task<IActionResult> UpdateTodo(int id, UpdateRequestViewModel todoItem)
        {
            try
            {
                TodoResponseViewModel result = await todoService.UpdateTodoAsync(id, todoItem);
                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "success",
                    Body = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }

        // DELETE: delete by id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            try
            {
                var result = await todoService.DeleteTodoAsync(id);
                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "success",
                    Body = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }

        // PUT: completed by id
        [HttpPut("complete-todo/{id}")]
        public async Task<IActionResult> GetCompletedTodoById(int id)
        {
            try
            {
                var result = await todoService.CompleteTodoByIdAsync(id);
                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "success",
                    Body = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }

        [HttpGet("get-completed-todo")]
        public async Task<IActionResult> GetAllCompletedTodo()
        {
            try
            {
                var result = await todoService.GetAllCompletedTodoAsync();

                var response = new ApiResponseViewModel
                {
                    Code = 200,
                    Message = "success",
                    Body = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }
    }
}
