namespace moutarde_back.Features.Auth.DTOs;

public record AuthResponse
{
    public required string Token;
    public required UserDto User;
}