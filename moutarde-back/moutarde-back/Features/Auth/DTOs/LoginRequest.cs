namespace moutarde_back.Features.Auth.DTOs;

public record LoginRequest
{
    public required string Email;
    public required string Password;
}