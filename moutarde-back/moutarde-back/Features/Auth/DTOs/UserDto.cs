using moutarde_back.Entities;

namespace moutarde_back.Features.Auth.DTOs;

public record UserDto
{
    public required Guid Id;
    public required string Email;
    public required string Username;

    public static UserDto FromUser(User user) => new UserDto()
    {
        Id = user.Id,
        Email = user.Email,
        Username = user.Username
    };
}