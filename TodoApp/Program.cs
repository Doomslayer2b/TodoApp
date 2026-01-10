using TodoApp.Models;
using Microsoft.EntityFrameworkCore;
using TodoApp.Mappings;

var builder = WebApplication.CreateBuilder(args);

// EF Core
builder.Services.AddDbContext<TodoDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer(); // minimal APIs need this
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TodoApp",
        Version = "3.1.0"  // Explicitly set OpenAPI version
    });
});

//Allows CORS for all origins, methods, and headers (for demo purposes)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.WithOrigins("http://localhost:5173")
                       .AllowAnyHeader()
                       .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowReact");
// Swagger middleware (only dev environment)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    });
}

// Map endpoints
RouteGroupBuilder todoItems = app.MapGroup("/todoitems");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete", GetCompleteTodos);
todoItems.MapGet("/{id}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/{id}", UpdateTodo);
todoItems.MapDelete("/{id}", DeleteTodo);

app.Run();

static async Task<IResult> GetAllTodos(TodoDb db) =>
    TypedResults.Ok(await db.Todos.Select(x => new TodoItemDTO(x)).ToArrayAsync());

static async Task<IResult> GetCompleteTodos(TodoDb db) =>
    TypedResults.Ok(await db.Todos.Where(t => t.IsComplete)
                                   .Select(x => new TodoItemDTO(x))
                                   .ToListAsync());

static async Task<IResult> GetTodo(int id, TodoDb db) =>
    await db.Todos.FindAsync(id) is Todo todo
        ? TypedResults.Ok(new TodoItemDTO(todo))
        : TypedResults.NotFound();

static async Task<IResult> CreateTodo(TodoItemDTO todoItemDTO, TodoDb db)
{
    if (string.IsNullOrWhiteSpace(todoItemDTO.Name))
    {
        return TypedResults.BadRequest("Name is required.");
    }
    var categoryExists = await db.Categories
        .AnyAsync(c => c.Id == todoItemDTO.CategoryId);

    if (!categoryExists)
    {
        return TypedResults.BadRequest("Invalid CategoryId");
    }

    var todoItem = new Todo
    {
        Name = todoItemDTO.Name,
        IsComplete = todoItemDTO.IsComplete,
        CategoryId = todoItemDTO.CategoryId
    };

    db.Todos.Add(todoItem);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/todoitems/{todoItem.Id}", new TodoItemDTO(todoItem));
}


static async Task<IResult> UpdateTodo(int id, TodoItemDTO todoItemDTO, TodoDb db)
{
    var categoryExists = await db.Categories
       .AnyAsync(c => c.Id == todoItemDTO.CategoryId);

    if (!categoryExists)
    {
        return TypedResults.BadRequest("Invalid CategoryId");
    }
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return TypedResults.NotFound();

    todo.Name = todoItemDTO.Name;
    todo.IsComplete = todoItemDTO.IsComplete;
    todo.CategoryId = todoItemDTO.CategoryId;
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteTodo(int id, TodoDb db)
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoDb>();

    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new Category { Name = "Work" },
            new Category { Name = "Personal" },
            new Category { Name = "Fitness" }
        );
        db.SaveChanges();
    }
}