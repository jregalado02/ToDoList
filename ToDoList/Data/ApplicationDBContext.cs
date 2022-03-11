using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<ToDo> ToDos { get; set; }

        public DbSet<ToDoCollection> ToDoCollection { get; set; }
    }
}
