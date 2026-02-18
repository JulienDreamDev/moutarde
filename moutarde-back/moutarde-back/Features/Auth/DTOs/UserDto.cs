using moutarde_back.Entities;

namespace moutarde_back.Features.Auth.DTOs;

public record UserDto
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Username { get; init; }

    public static UserDto FromUser(User user) => new UserDto()
    {
        Id = user.Id,
        Email = user.Email,
        Username = user.Username
    };
}