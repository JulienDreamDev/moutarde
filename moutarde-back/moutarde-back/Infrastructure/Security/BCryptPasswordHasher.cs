namespace moutarde_back.Infrastructure.Security;

using BCrypt.Net;

public class BCryptPasswordHasher : IPasswordHasher
{
    public static string Hash(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public static bool Verify(string password, string hash)
    {
        return BCrypt.Verify(password, hash);
    }
}