using Microsoft.EntityFrameworkCore;

namespace TodoApi; 
public class TodoDb : DbContext {
    public TodoDb(DbContextOptions<TodoDb> options) : base(options) { } // Constructor to pass options to the base class
    public DbSet<TodoItem> Todos => Set<TodoItem>(); // Property to access the TodoItems table in the database
}
