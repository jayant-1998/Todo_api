using Microsoft.EntityFrameworkCore;
using TodoAPI.DAL.DBContexts;
using TodoAPI.DAL.Repositories.Implementations;
using TodoAPI.DAL.Repositories.Interfaces;
using TodoAPI.Services.Implementation;
using TodoAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ITodoRepository , TodoRepository>();
builder.Services.AddTransient<ITodoService, TodoService>();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
