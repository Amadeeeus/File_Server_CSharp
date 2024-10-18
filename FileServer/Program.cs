using FileServer.Extensions;
using FileServer.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
builder.Services.AddDbContext<AppDbContext>(options=>options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.Run();
