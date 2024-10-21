using AutoMapper;
using FileServer.Extensions;
using FileServer.Handlers;
using FileServer.Mappers;
using FileServer.Models.DTOs;
using FileServer.Repositories.Implementations;
using FileServer.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static FileServer.Extensions.ServiceExtentions;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
builder.Services.AddDbContext<AppDbContext>(options=>options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION")));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//builder.Services.AddScoped<PasswordHasher<FileDTO>>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddServices();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddControllers();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/");
app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.Run();
