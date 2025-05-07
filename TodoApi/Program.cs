using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

// Add DI
builder.Services.AddDbContext<TodoDb>(options => options.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Configure pipeline - UseMethod..
app.MapPost("/AddNewTodoItem", async (TodoDb db, TodoItem item) => { 
    db.Todos.Add(item); // Add the new item to the database
    await db.SaveChangesAsync(); // Save changes to the database
    return Results.Created($"/{item.Id}", item); // Return a 201 Created response with the location of the new item

});
app.MapGet("/GetAllTodoItems", async (TodoDb db) => {
    return await db.Todos.ToListAsync(); // Return all items in the database
});
app.MapGet("/GetTodoItem/{id}", async (TodoDb db, int id) => {
    var item = await db.Todos.FindAsync(id); // Find the item by ID
    if (item is null) return Results.NotFound(); // Return 404 if not found
    return item is not null ? Results.Ok(item) : Results.NotFound(); // Return the item or 404 if not found
});
app.MapPut("/UpdateTodoItem/{id}", async (TodoDb db, int id, TodoItem updatedItem) => {
    var item = await db.Todos.FindAsync(id); // Find the item by ID
    if (item is null) return Results.NotFound(); // Return 404 if not found
    item.Name = updatedItem.Name; // Update the item's properties
    item.IsComplete = updatedItem.IsComplete;
    item.CompletedAt = updatedItem.IsComplete ? DateTime.UtcNow : null;
    await db.SaveChangesAsync(); // Save changes to the database
    return Results.NoContent(); // Return 204 No Content
});
app.MapDelete("/DeleteTodoItem/{id}", async (TodoDb db, int id) => {
    var item = await db.Todos.FindAsync(id); // Find the item by ID
    if (item is null) return Results.NotFound(); // Return 404 if not found
    db.Todos.Remove(item); // Remove the item from the database
    await db.SaveChangesAsync(); // Save changes to the database
    return Results.NoContent(); // Return 204 No Content
});


app.Run();
