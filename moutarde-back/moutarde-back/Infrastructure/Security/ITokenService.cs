using moutarde_back.Entities;

namespace moutarde_back.Infrastructure.Security;

public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token for the given user.
    /// </summary>
    /// <param name="user">The user data used to generate the token</param>
    /// <returns>The user's JWT token</returns>
    public string GenerateToken(User user);
}