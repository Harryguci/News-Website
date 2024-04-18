using Domain.Domain.Repositories.Implement;
using Domain.Domain.Repositories.Interface;
using Domain.Helpers;
using Domain.Middleware;
using Domain.Services.Implement;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.Domain.Repositories.Interface;
using NewsWebsite.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NewsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INewsPostRepository, NewsPostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var myAllowSpecificOrigins = "news-website";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000",
            "https://localhost:3001",
            "http://localhost:3001").AllowAnyHeader().AllowAnyMethod();
    });
});

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
