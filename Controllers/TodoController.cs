using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;
using TodoAPI.Services.Interface;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _services;

        public TodoController(ITodoServices services)
        {
            _services = services;
        }



        // GET: show all todos
        [HttpGet]
        public async Task<ActionResult> GetTodoItems()
        {
            try
            {
                var result = await _services.FetchAllDataAsync();
                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
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
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
                return Ok(response);
            }




        }

        // GET: show todo by id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTodoItem(int id)
        {
            try
            {
                var result = await _services.FetchAllDataByIdAsync(id);
                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
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
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
                return Ok(response);
            }
        }

        // POST: insert data
        [HttpPost]
        public async Task<ActionResult> CreateTodoItem(RequestModels todoItem)
        {
            try
            {
                var result = await _services.CreateTodo(todoItem);

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 200,
                    Message = "Success",
                    Body = result
                };


                return Ok(response);
            }
            catch (DbUpdateException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception message available";

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
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

                return Ok(response);
            }
        }

        // PUT: update todo by id 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, RequsetUpdateModels todoItem)
        {
            try
            {
                var result = await _services.Updatetodo(id, todoItem);  

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
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
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }

        // DELETE: delete by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
            {
                var result = await _services.DeleteTodo(id);

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
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
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }

        // PUT: completed by id
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteTodoItem(int id)
        {
            try
            {
                var result = await _services.GetCompletedById(id);

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
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
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }

        [HttpGet("completed")]
        public async Task<ActionResult> GetCompletedTodoItems()
        {
            try
            {
                var result = await _services.GetAllCompleted();

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
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
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };

                return Ok(response);
            }
        }
    }
}