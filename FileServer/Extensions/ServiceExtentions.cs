using AutoMapper;
using FileServer.Controllers;
using FileServer.Jobs;
using FileServer.Models.DTOs;
using FileServer.Repositories.Implementations;
using FileServer.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Quartz;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FileServer.Extensions;

public static class ServiceExtentions
{
    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" }));
    }

    public static void AddQuartz(this IServiceCollection services)
    {
        services.AddQuartz(opt =>
        {
            var jobkey = new JobKey("DeleteFileJob");
            opt.AddJob<DeleteFileJob>(q => q.WithIdentity(jobkey));

            opt.AddTrigger(t => t.ForJob(jobkey).WithIdentity("DeleteFileJob-Trigger").WithCronSchedule("0 0 12 1/1 * ? *"));
        });
        services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<PasswordHasher<FileDTO>>();
        services.AddScoped<PasswordHasher>();
    }
}