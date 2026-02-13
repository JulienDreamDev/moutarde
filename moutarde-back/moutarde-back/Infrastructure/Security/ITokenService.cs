using moutarde_back.Entities;

namespace moutarde_back.Infrastructure.Security;

public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token for the given user.
    /// </summary>
    /// <param name="user">The user data</param>
    /// <returns>The JWT token</returns>
    public string GenerateToken(User user);
}