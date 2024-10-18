using FileServer.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FileServer.Extensions;

public class AppDbContext: DbContext
{
    public DbSet<FileDTO> Files { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
        
    }
}