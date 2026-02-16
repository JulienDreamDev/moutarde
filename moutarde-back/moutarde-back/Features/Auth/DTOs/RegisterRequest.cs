namespace moutarde_back.Features.Auth.DTOs;

public class RegisterRequest
{
    public required string Email;
    public required string Username;
    public required string Password;
}