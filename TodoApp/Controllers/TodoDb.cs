using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Todos)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId);
    }
}
