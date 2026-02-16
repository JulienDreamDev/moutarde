using Microsoft.EntityFrameworkCore;
using moutarde_back.Entities;
using moutarde_back.Features.Auth.DTOs;
using moutarde_back.Infrastructure.Data;
using moutarde_back.Infrastructure.Security;

namespace moutarde_back.Features.Auth;

public partial class AuthService(
    MoutardeDbContext dbContext,
    IPasswordHasher hasher,
    ITokenService tokenService,
    ILogger<AuthService> logger
    ) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        LogRegisterAttempt(request.Email);
        
        if (await dbContext.Users.AnyAsync(user => user.Email == request.Email))
        {
            LogRegisterAttemptFailed(request.Email);
            throw new InvalidOperationException("Email already in use.");
        }

        var hash = hasher.Hash(request.Password);

        var user = User.Create(request.Username, request.Email, hash);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        LogRegisterAttemptSuccessful(user.Id);
        
        var token = tokenService.GenerateToken(user);
        
        return new AuthResponse()
        {
            Token =  token,
            User = UserDto.FromUser(user)
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        LogLoginAttempt(request.Email);
        
        var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email);

        if (user is null || !hasher.Verify(request.Password, user.PasswordHash))
        {
            LogLoginAttemptFailed();
            throw new UnauthorizedAccessException("Email or password is incorrect."); // Avoid giving hints about which one is wrong.
        }
        
        LogLoginAttemptSuccessful(user.Id);
        
        var token = tokenService.GenerateToken(user);

        return new AuthResponse()
        {
            Token = token,
            User = UserDto.FromUser(user)
        };
    }
    
    [LoggerMessage(Level = LogLevel.Information, Message = "Registering user with email {email}")]
    private partial void LogRegisterAttempt(string email);
    [LoggerMessage(Level = LogLevel.Warning, Message = "Registration failed: Email {email} is already in use")]
    private partial void LogRegisterAttemptFailed(string email);
    [LoggerMessage(Level = LogLevel.Information, Message = "User {uid} successfully registered")]
    private partial void LogRegisterAttemptSuccessful(Guid uid);
    
    [LoggerMessage(Level = LogLevel.Information, Message = "Signing in user with email {email}")]
    private partial void LogLoginAttempt(string email);
    [LoggerMessage(Level = LogLevel.Warning, Message = "Signing failed: Email or password incorrect")]
    private partial void LogLoginAttemptFailed();
    [LoggerMessage(Level = LogLevel.Information, Message = "User {uid} successfully logged in")]
    private partial void LogLoginAttemptSuccessful(Guid uid);
}