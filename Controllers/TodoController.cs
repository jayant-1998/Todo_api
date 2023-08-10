using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;
using TodoAPI.Services.Interface;

namespace TodoAPI.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(IServiceProvider service)
        {
            _service = service.GetService<ITodoService>();  
        }

        // GET: show all todo
        [Route("get-all-todo")]
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            try
            {
                IEnumerable<TodoResponseViewModel> result = await _service.GetAllTodoAsync();
               
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
        public async Task<ActionResult> GetTodoById(int id)
        {
            try
            {
                TodoResponseViewModel result = await _service.GetTodoByIdAsync(id);
                
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
        public async Task<ActionResult> InsertTodo(InsertRequestViewModel todoItem)
        {
            try
            {
                var result = await _service.InsertTodoAsync(todoItem);

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
                TodoResponseViewModel result = await _service.UpdateTodoAsync(id, todoItem); 
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
                var result = await _service.DeleteTodoAsync(id);
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
                var result = await _service.CompleteTodoByIdAsync(id);
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
        public async Task<ActionResult> GetAllCompletedTodo()
        {
            try
            {
                var result = await _service.GetAllCompletedTodoAsync();

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