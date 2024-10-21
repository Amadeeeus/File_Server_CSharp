using AutoMapper;
using FileServer.Controllers;
using FileServer.Models.DTOs;
using FileServer.Repositories.Implementations;
using FileServer.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FileServer.Extensions;

public static class ServiceExtentions
{
    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" }));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<PasswordHasher<FileDTO>>();
        services.AddScoped<HashingString>();
    }
}