using Microsoft.EntityFrameworkCore;
using TodoAPI.DAL.DBContexts;
using TodoAPI.DAL.Entity;
using TodoAPI.DAL.Repositories.Interfaces;
using TodoAPI.Extensions;
using TodoAPI.Models.RequestViewModels;
using TodoAPI.Models.ResponseViewModels;

namespace TodoAPI.DAL.Repositories.Implementations
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public TodoRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoItem> InsertTaskAsync(InsertRequestViewModel todoItem)
        {
            var todoItemEntity = todoItem.ToViewModel<InsertRequestViewModel, TodoItem>();

            await _dbContext.AddAsync(todoItemEntity);
            await _dbContext.SaveChangesAsync();

            return todoItemEntity;
        }

        public async Task<string> DeleteTaskAsync(int id)
        {
            var todoItem = await _dbContext.TodoItems
                .Where(todo => todo.ID == id && !todo.IsDeleted)
                .FirstOrDefaultAsync();

            if (todoItem == null)
            {
                return "id:"+id + " Not Found in the Database";
            }
            todoItem.IsDeleted = true;
            todoItem.DeletedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return "id:" + id + " is Successfully Deleted";
        }
    
        public async Task<IEnumerable<TaskResponseViewModel?>> GetAllTasksAsync()
        {
            IEnumerable<TodoItem> todoItems = await _dbContext.TodoItems
                            .Where(todo => !todo.IsCompleted && !todo.IsDeleted)
                            .ToListAsync();

            if (todoItems == null)
            {
                return null;
            }

            List<TaskResponseViewModel> respond = new List<TaskResponseViewModel>();

            foreach (var item in todoItems)
            {
                var response = item.ToViewModel<TodoItem, TaskResponseViewModel>();
                respond.Add(response);
            }

            return respond;
        }

        public async Task<TaskResponseViewModel?> GetTaskByIdAsync(int id)
        {
            TodoItem todoItem = await _dbContext.TodoItems
                            .Where(todo => todo.ID == id && !todo.IsCompleted && !todo.IsDeleted)
                            .FirstOrDefaultAsync();

            if (todoItem == null)
            {
                return null;
            }
            var respond = todoItem.ToViewModel<TodoItem , TaskResponseViewModel>();


            return respond;
        }

        public async Task<IEnumerable<TaskResponseViewModel?>> GetAllCompleteTasksAsync()
        {
            IEnumerable<TodoItem> todoItems = await _dbContext.TodoItems
                .Where(t => t.IsCompleted == true && !t.IsDeleted).ToListAsync();


            if (todoItems == null)
            {
                return null;
            }

            List<TaskResponseViewModel> respond = new List<TaskResponseViewModel>();

            foreach (var item in todoItems)
            {
                var response = item.ToViewModel<TodoItem, TaskResponseViewModel>();
                respond.Add(response);
            }

            return respond;
        }

        public async Task<TaskResponseViewModel?> CompleteTaskAsync(int id)
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

            var respond = item.ToViewModel<TodoItem, TaskResponseViewModel>();
            
            return respond;
            
        }

        public async Task<TaskResponseViewModel?> UpdateTaskAsync(int id, UpdateRequestViewModel todoItem)
        {
            var body = await _dbContext.TodoItems
                .Where(todo => todo.ID == id && !todo.IsCompleted && !todo.IsDeleted)
                .FirstOrDefaultAsync();
            if (body == null)
            {
                return null;
            }
            body.Name = todoItem.Name;
            body.Description = todoItem.Description;
            body.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            
            var respond = todoItem.ToViewModel<UpdateRequestViewModel, TaskResponseViewModel>();
            respond.ID = id;
            return respond;
        }
    }
}
