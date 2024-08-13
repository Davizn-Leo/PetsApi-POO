using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaHotApi.Data;
using PizzaHotApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("animaldb"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/animais{id}", async (AppDbContext db) => await db.Animais.ToListAsync());
app.MapPut("/animais{id}", async (AppDbContext db,Animais updateanimal, int id) =>
{
    var animal = await db.Animais.FindAsync(id);
    if (animal is null) return Results.NotFound();
    animal.Nome = updateanimal.Nome;
    animal.Idade = updateanimal.Idade;
    animal.Peso = updateanimal.Peso;
    animal.Tipo = updateanimal.Tipo;
    animal.Cor = updateanimal.Cor;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/animal{id}", async (AppDbContext db, int id) =>
{
    var animal= await db.Animais.FindAsync(id);
    if (animal is null) { return Results.NotFound(); }
    db.Animais.Remove(animal);
    db.Animais.Remove(animal);

    await db.SaveChangesAsync();
    return Results.Ok();
});
app.MapGet("/animais", async (AppDbContext db) => await db.Animais.ToListAsync());
app.MapPost("/animal", async (AppDbContext db, Animais animal) =>
{
    await db.Animais.AddAsync(animal);
    await db.SaveChangesAsync();
    return Results.Created($"/animal/{animal.Id}", animal);
});
app.Run();
