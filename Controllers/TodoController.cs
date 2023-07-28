using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.DAL.DBContexts;
using TodoAPI.DAL.Entity;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TodoController(ApplicationDBContext context)
        {
            _context = context;
        }



        // GET: show all todos
        [HttpGet]
        public ActionResult<IEnumerable<ResponseTodoItem>> GetTodoItems()
        {
            try
            {
                var body = _context.TodoItems
                            .Where(todo => todo.IsCompleted == false && todo.IsDeleted == false);

                var responsetodoitem = body.Select(todo => new ResponseTodoItem
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

                return responsetodoitem;
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = "Have A Exception",
                    Body = ex.Message
                };
                return BadRequest(response);
            }




        }

        // GET: show todo by id
        [HttpGet("{id}")]
        public ActionResult<ResponseTodoItem> GetTodoItem(int id)
        {
            try
            {
                var todoItem = _context.TodoItems
                    .Where(todo => todo.ID == id && todo.IsCompleted == false && todo.IsDeleted == false)
                    .FirstOrDefault();

                if (todoItem == null)
                {
                    return NotFound();
                }

                var responsetodoitem = new ResponseTodoItem
                {
                    ID = todoItem.ID,
                    Name = todoItem.Name,
                    Description = todoItem.Description,
                    IsCompleted = todoItem.IsCompleted,
                    DeletedAt = todoItem.DeletedAt,
                    UpdatedAt = todoItem.UpdatedAt,
                    CreatedAt = todoItem.CreatedAt,
                    IsDeleted = todoItem.IsDeleted,
                    CompletedAt = todoItem.CompletedAt,

                };

                return responsetodoitem;
            }
            catch (Exception ex)
            {
                var respose = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = "Have A Exception",
                    Body = ex.Message
                };

                return BadRequest(respose);
            }
        }

        // POST: insert data
        [HttpPost]
        public ActionResult<ApiResponseViewModel> CreateTodoItem(RequestModels todoItem)
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


                _context.Add(requestbody);
                _context.SaveChanges();

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 200,
                    Message = "Success",
                    Body = "Request processed successfully."
                };


                return Ok(response);
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

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = "Have An Exception",
                    Body = $"{ex.Message}"
                };

                return BadRequest(response);
            }
        }

        // PUT: update todo by id 
        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(int id, RequsetUpdateModels todoItem)
        {
            try
            {
                var body = _context.TodoItems
                            .Where(todo => todo.ID == id && todo.IsCompleted == false && todo.IsDeleted == false)
                            .SingleOrDefault();

                if (body == null)
                {
                    return NotFound();
                }

                body.Name = todoItem.Name;
                body.Description = todoItem.Description;
                body.UpdatedAt = DateTime.Now;

                _context.SaveChanges();

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 200,
                    Message = "Success",
                    Body = "Request processed successfully."
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var respose = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = "Have A Exception",
                    Body = ex.Message
                };

                return BadRequest(respose);
            }
        }

        // DELETE: delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            try
            {
                var todoItem = _context.TodoItems
                                .Where(todo => todo.ID == id && todo.DeletedAt == null)
                                .FirstOrDefault();

                if (todoItem == null)
                {
                    return NotFound();
                }

                todoItem.DeletedAt = DateTime.Now;
                todoItem.IsDeleted = true;
                _context.SaveChanges();
                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 200,
                    Message = "Success",
                    Body = "Request processed successfully."
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                var respose = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = "Have A Exception",
                    Body = ex.Message
                };

                return BadRequest(respose);
            }
        }

        // PUT: completed by id
        [HttpPut("{id}/complete")]
        public IActionResult CompleteTodoItem(int id)
        {
            try
            {
                var todoItem = _context.TodoItems
                                .Where(todo => todo.ID == id)
                                .FirstOrDefault();

                if (todoItem == null && todoItem.DeletedAt == null)
                {
                    return NotFound();
                }

                todoItem.CompletedAt = DateTime.Now;
                todoItem.IsCompleted = true;
                _context.SaveChanges();

                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 200,
                    Message = "Success",
                    Body = "Request processed successfully."
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = "have a exception",
                    Body = ex.Message
                };


                return BadRequest(response);
            }
        }

        [HttpGet("completed")]
        public ActionResult<IEnumerable<ResponseTodoItem>> GetCompletedTodoItems()
        {
            try
            {
                var body = _context.TodoItems
                            .Where(todo => todo.IsCompleted != false && todo.IsDeleted == false);

                var responsetodoitem = body.Select(todo => new ResponseTodoItem
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

                return responsetodoitem;
            }
            catch (Exception ex)
            {
                var response = new ApiResponseViewModel
                {
                    Timestamp = DateTime.Now,
                    Code = 500,
                    Message = "Have A Exception",
                    Body = ex.Message
                };
                return BadRequest(response);
            }
        }
    }
}