using Microsoft.EntityFrameworkCore;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.BusinessLogicLayer;
using HeroAPI.DataAccesLayer.Repositories;
using HeroAPI.DataAccessLayer.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<HeroContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHeroService, HeroService>(); 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHeroRepository, HeroRepository>(); 
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
