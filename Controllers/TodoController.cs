using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;
using TodoAPI.Services.Interface;

namespace TodoAPI.Controllers
{
    
    [Route("todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        // GET: show all todos
        [Route("get-task-items")]
        [HttpGet]
        public async Task<ActionResult> GetTaskItems()
        {
            try
            {
                IEnumerable<TaskResponseViewModel> result = await _service.GetAllTasksAsync();
               
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
        [HttpGet("get-task-item/{id}")]
        public async Task<ActionResult> GetTaskItemsById(int id)
        {
            try
            {
                TaskResponseViewModel result = await _service.GetTaskByIdAsync(id);
                
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
        [Route("insert-task")]
        [HttpPost]
        public async Task<ActionResult> InsertTask(InsertRequestViewModel todoItem)
        {
            try
            {
                var result = await _service.InsertTaskAsync(todoItem);

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
        [HttpPut("update-task/{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, UpdateRequestViewModel todoItem)
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
        public async Task<IActionResult> DeleteTaskItem(int id)
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
        [HttpPut("completed-task/{id}")]
        public async Task<IActionResult> GetCompletedTaskItemById(int id)
        {
            try
            {
                var result = await _service.CompletedTaskAsync(id);

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

        [HttpGet("completed-task")]
        public async Task<ActionResult> GetCompletedTaskItems()
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