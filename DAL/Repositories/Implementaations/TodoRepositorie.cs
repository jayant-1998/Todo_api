using Microsoft.EntityFrameworkCore;
using TodoAPI.DAL.DBContexts;
using TodoAPI.DAL.Entity;
using TodoAPI.DAL.Repositories.Interfaces;
using TodoAPI.Extensions;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.DAL.Repositories.Implementaations
{
    public class TodoRepositories : ITodoRepositorie
    {
        private readonly ApplicationDBContext _dbContext;

        public TodoRepositories(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoItem> CreateTaskAsync(RequestModel todoItem)
        {

            TodoItem requestbody = new TodoItem
            {
                ID = todoItem.Id,
                Name = todoItem.Name,
                Description = todoItem.Description,
                CreatedAt = DateTime.Now,
            };

            await _dbContext.AddAsync(requestbody);
            await _dbContext.SaveChangesAsync();

            return requestbody;
        }

        public async Task<string> DeleteTaskByIdDbAsync(int id)
        {
            var todoItem = await _dbContext.TodoItems
                .Where(todo => todo.ID == id && !todo.IsDeleted)
                .FirstOrDefaultAsync();

            if (todoItem != null)
            {
                return "id:"+id + " Not Found in the Database";
            }
            todoItem.IsDeleted = true;
            todoItem.DeletedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return "id:" + id + " is Succesfully Delected";
        }

        public async Task<IEnumerable<ResponseTodoItem?>> GetAllTasksFromDbAsync()
        {
            IEnumerable<TodoItem> todoItems = await _dbContext.TodoItems
                            .Where(todo => !todo.IsCompleted && !todo.IsDeleted)
                            .ToListAsync();

            if (todoItems == null)
            {
                return null;
            }

            List<ResponseTodoItem> responseTodoItems = new List<ResponseTodoItem>();

            foreach (var item in todoItems)
            {
                var response = item.ToViewModel<TodoItem, ResponseTodoItem>();
                responseTodoItems.Add(response);
            }

            return responseTodoItems;
        }

        public async Task<ResponseTodoItem?> GetTaskByIdDbAsync(int id)
        {
            TodoItem todoItem = await _dbContext.TodoItems
                            .Where(todo => todo.ID == id && !todo.IsCompleted && !todo.IsDeleted)
                            .SingleOrDefaultAsync();

            if (todoItem == null)
            {
                return null;
            }
            var responseTodoItems = todoItem.ToViewModel<TodoItem , ResponseTodoItem>();


            return responseTodoItems;
        }

        public async Task<IEnumerable<ResponseTodoItem?>> GetAllCompleteTasksDbAsync()
        {
            IEnumerable<TodoItem> todoItems = await _dbContext.TodoItems
                .Where(t => t.IsCompleted != false && !t.IsDeleted).ToListAsync();


            if (todoItems == null)
            {
                return null;
            }

            List<ResponseTodoItem> responseTodoItems = new List<ResponseTodoItem>();

            foreach (var item in todoItems)
            {
                var response = item.ToViewModel<TodoItem, ResponseTodoItem>();
                responseTodoItems.Add(response);
            }

            return responseTodoItems;
        }

        public async Task<ResponseTodoItem?> CompleteTaskByIdDbAsync(int id)
        {
            TodoItem item = await _dbContext.TodoItems
                .Where (todo => todo.ID == id && !todo.IsCompleted && !todo.IsDeleted)
                .SingleOrDefaultAsync();
            if (item == null)
            {
                return null;
            }
            item.CompletedAt = DateTime.Now;
            item.IsCompleted = true;
            await _dbContext.SaveChangesAsync();

            var respons = item.ToViewModel<TodoItem, ResponseTodoItem>();
            
            return respons;
            
        }

        public async Task<ResponseTodoItem?> UpdateTasksDbAsync(int id, RequsetUpdateModel todoItem)
        {
            var body = await _dbContext.TodoItems
                .Where(todo => todo.ID == id && !todo.IsCompleted && !todo.IsDeleted)
                .SingleOrDefaultAsync();
            if (body == null)
            {
                return null;
            }
            body.Name = todoItem.Name;
            body.Description = todoItem.Description;
            body.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            var response = new ResponseTodoItem
            {
                ID = id,
                Name = todoItem.Name,
                Description = todoItem.Description,
                CreatedAt = DateTime.Now,
            };
            return response;
        }
    }
}
