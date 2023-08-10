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

        public async Task<TodoItem> InsertTodoAsync(InsertRequestViewModel todoItem)
        {
            var todoItemEntity = todoItem.ToViewModel<InsertRequestViewModel, TodoItem>();

            await _dbContext.AddAsync(todoItemEntity);
            await _dbContext.SaveChangesAsync();

            return todoItemEntity;
        }

        public async Task<TodoItem> DeleteTodoAsync(int id)
        {
            TodoItem todoItem = await _dbContext.TodoItems
                .Where(todo => todo.ID == id)
                .FirstOrDefaultAsync();

            if (todoItem != null)
            {
                todoItem.IsDeleted = true;
                todoItem.DeletedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return todoItem;
            }
            return null;
        }

        public async Task<IEnumerable<TodoResponseViewModel?>> GetAllTodoAsync()
        {
            IEnumerable<TodoItem> todoItems = await _dbContext.TodoItems
                            .Where(todo => !todo.IsDeleted && !todo.IsCompleted)
                            .ToListAsync();

            if (todoItems != null)
            {
                List<TodoResponseViewModel> response = new List<TodoResponseViewModel>();

                foreach (var item in todoItems)
                {
                    var temp = item.ToViewModel<TodoItem, TodoResponseViewModel>();
                    response.Add(temp);
                }

                return response;
            }

            return null;
        }

        public async Task<TodoResponseViewModel?> GetTodoByIdAsync(int id)
        {
            TodoItem todoItem = await _dbContext.TodoItems
                            .Where(todo => todo.ID == id && !todo.IsDeleted)
                            .FirstOrDefaultAsync();

            if (todoItem == null)
            {
                return null;
            }
            var response = todoItem.ToViewModel<TodoItem, TodoResponseViewModel>();


            return response;
        }

        public async Task<IEnumerable<TodoResponseViewModel?>> GetAllCompletedTodoAsync()
        {
            IEnumerable<TodoItem> todoItems = await _dbContext.TodoItems
                .Where(t => t.IsCompleted && !t.IsDeleted).ToListAsync();


            if (todoItems == null)
            {
                return null;
            }

            List<TodoResponseViewModel> response = new List<TodoResponseViewModel>();

            foreach (var item in todoItems)
            {
                var temp = item.ToViewModel<TodoItem, TodoResponseViewModel>();
                response.Add(temp);
            }

            return response;
        }

        public async Task<TodoResponseViewModel?> CompleteTodoByIdAsync(int id)
        {
            TodoItem item = await _dbContext.TodoItems
                .Where(todo => todo.ID == id && !todo.IsDeleted && !todo.IsCompleted)
                .SingleOrDefaultAsync();
            if (item == null)
            {
                return null;
            }
            if (item.IsCompleted)
            {
                throw new InvalidOperationException("already completed");
            }
            item.CompletedAt = DateTime.Now;
            item.IsCompleted = true;
            await _dbContext.SaveChangesAsync();

            var response = item.ToViewModel<TodoItem, TodoResponseViewModel>();

            return response;

        }

        public async Task<TodoResponseViewModel> UpdateTodoAsync(int id, UpdateRequestViewModel todoItem)
        {
            var item = await _dbContext.TodoItems
                .Where(todo => todo.ID == id && !todo.IsDeleted)
                .FirstOrDefaultAsync();

            if (item != null)
            {
                item.Name = todoItem.Name;
                item.Description = todoItem.Description;
                item.UpdatedAt = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                var response = todoItem.ToViewModel<UpdateRequestViewModel, TodoResponseViewModel>();
                response.ID = id;
                return response;
            }

            return null;
        }
    }
}
