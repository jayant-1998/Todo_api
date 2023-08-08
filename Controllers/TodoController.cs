using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;
using TodoAPI.Services.Interface;

namespace TodoAPI.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        // GET: show all todos
        [Route("GetAllTasks")]
        [HttpGet]
        public async Task<ActionResult> GetTodoItems()
        {
            try
            {
                IEnumerable<ResponseTodoItem> result = await _service.GetAllTasksAsync();
               
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
        [HttpGet("{id}/GetTaskById")]
        public async Task<ActionResult> GetTodoItem(int id)
        {
            try
            {
                ResponseTodoItem result = await _service.GetTaskByIdAsync(id);
                
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
        [Route("InsertTask")]
        [HttpPost]
        public async Task<ActionResult> CreateTodoItem(RequestModel todoItem)
        {
            try
            {
                var result = await _service.CreateTaskAsync(todoItem);

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

        // PUT: update todo by id 
        [HttpPut("{id}/UpdateTask")]
        public async Task<IActionResult> UpdateTodoItem(int id, RequsetUpdateModel todoItem)
        {
            try
            {
                var result = await _service.UpdateTaskAsync(id, todoItem); 
                
                if (result == null)
                {
                    throw new Exception("id is not found in database or it already deleted");
                }

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

        // DELETE: delete by id
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
            {
                var result = await _service.DeleteTaskAsync(id);
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

        // PUT: completed by id
        [HttpPut("{id}/CompleteTaskById")]
        public async Task<IActionResult> CompleteTodoItem(int id)
        {
            try
            {
                var result = await _service.CompleteTaskByIdAsync(id);

                if (result == null)
                {
                    throw new Exception("id is not found in database");
                }

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

        [HttpGet("GetAllCompletedTask")]
        public async Task<ActionResult> GetCompletedTodoItems()
        {
            try
            {
                var result = await _service.GetAllCompleteTasksAsync();

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
    }
}