namespace moutarde_back.Infrastructure.Security;

using BCrypt.Net;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        try
        {
            return BCrypt.Verify(password, hash);
        }
        catch (SaltParseException) // This exception is thrown when the hash is not in the correct format (e.g., not a valid bcrypt hash)
        {
            return false;
        }
    }
}