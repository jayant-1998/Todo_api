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
        [Route("get-task-items")]
        [HttpGet]
        public async Task<IActionResult> GetTaskItems()
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
                TaskResponseViewModel result = await _service.UpdateTaskAsync(id, todoItem); 
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