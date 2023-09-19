using TodoAPI.DAL.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DAL.Repositories.Implementations;
using TodoAPI.DAL.Repositories.Interfaces;
using TodoAPI.Services.Implementation;
using TodoAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddApiVersioning(setup =>
{
    var v = new Version("1.0");
    setup.DefaultApiVersion = new ApiVersion(v.Major,v.Minor);
    //setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
    setup.UseApiBehavior = true;
    setup.ApiVersionReader = new QueryStringApiVersionReader("api-version");
    //setup.ApiVersionReader = new UrlSegmentApiVersionReader();
});

//builder.Services.AddVersionedApiExplorer(setup =>
//{
//    setup.GroupNameFormat = "'v'VVV";
//    setup.SubstituteApiVersionInUrl= true;
//});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer("name=DefaultConnection"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ITodoRepository , TodoRepository>();
builder.Services.AddTransient<ITodoService, TodoServiceV1>();
builder.Services.AddTransient(typeof(TodoServiceV2)); //this same interface for multiple services

builder.Services.AddSwaggerGen();


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
