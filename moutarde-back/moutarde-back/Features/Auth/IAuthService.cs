using moutarde_back.Features.Auth.DTOs;

namespace moutarde_back.Features.Auth;

public interface IAuthService
{
    /// <summary>
    /// Register a new user and return an authentication response containing a JWT token and user information.
    /// </summary>
    /// <param name="request">The user information to register</param>
    /// <returns>A created User and a JWT token</returns>
    public Task<AuthResponse> RegisterAsync(RegisterRequest request);
    
    /// <summary>
    /// Login an existing user and return an authentication response containing a JWT token and user information.
    /// </summary>
    /// <param name="request">The user information to login</param>
    /// <returns>The corresponding User and a JWT token</returns>
    public Task<AuthResponse> LoginAsync(LoginRequest request);
}