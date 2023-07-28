using Microsoft.EntityFrameworkCore;
using TodoAPI.DAL.Entity;

namespace TodoAPI.DAL.DBContexts
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
