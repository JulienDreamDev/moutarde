namespace moutarde_back.Features.Auth.DTOs;

public record RegisterRequest
{
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}