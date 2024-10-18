namespace FileServer.Models.Entities;

public class Admin
{
    public string Password { get; set; } = Environment.GetEnvironmentVariable("AdminPassword");
}