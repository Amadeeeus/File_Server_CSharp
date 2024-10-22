using System.Security.Cryptography;
using System.Text;

namespace FileServer.Extensions;

public class PasswordHasher
{
    public string adminPassword { get; set; } = Environment.GetEnvironmentVariable("AdminPassword");

    public string HashString(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return Convert.ToHexString(sha256.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
    }

    public bool VerifyHashAdmin(string input)
    {
        var hashAdmin = HashString(adminPassword);
        var hashInput = HashString(input);
        if (!hashAdmin.Equals(hashInput))
        {
            throw new UnauthorizedAccessException("Invalid password for administrator");
        }
        return true;
    }
    public bool VerifyHash(string basePassword, string inputPassword)
    {
        var hashBase = HashString(basePassword);
        var hashInput = HashString(inputPassword);
        if (!hashBase.Equals(hashInput))
        {
            throw new UnauthorizedAccessException("Invalid password");
        }
        return true;
    }
}