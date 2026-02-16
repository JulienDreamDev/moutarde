using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;
using moutarde_back.Entities;
using moutarde_back.Features.Auth;
using moutarde_back.Features.Auth.DTOs;
using moutarde_back.Infrastructure.Data;
using moutarde_back.Infrastructure.Security;

namespace moutarde_tests;

public class AuthServiceTests : IDisposable
{
    private readonly MoutardeDbContext _context;
    private readonly IAuthService _authService;
    private readonly Mock<ITokenService> _tokenService;
    private readonly Mock<IPasswordHasher> _passwordHasher;

    public AuthServiceTests()
    {
        var options = new DbContextOptionsBuilder<MoutardeDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        _tokenService = new Mock<ITokenService>();
        _passwordHasher = new Mock<IPasswordHasher>();
        var loggerAuthService = new Mock<ILogger<AuthService>>();
        var loggerMoutardeDbContext = new Mock<ILogger<MoutardeDbContext>>();
        
        _context = new MoutardeDbContext
            (
                options: options,
                logger: loggerMoutardeDbContext.Object
            );
        
        _authService = new AuthService
            (
                dbContext: _context,
                tokenService: _tokenService.Object,
                hasher: _passwordHasher.Object,
                logger: loggerAuthService.Object
            );
    }

    [Fact]
    public async Task RegisterAsync_ShouldCreateUserAndReturnAuthResponse()
    {
        var request = new RegisterRequest()
        {
            Email = "test@email.com",
            Username = "testUser",
            Password = "testPassword"
        };
        
        _passwordHasher.Setup(h => h.Hash(request.Password)).Returns("fakeHash");
        _tokenService.Setup(ts => ts.GenerateToken(It.IsAny<User>())).Returns("fakeToken");
        
        var response = await _authService.RegisterAsync(request);

        Assert.NotNull(response);
        Assert.Equal("fakeToken", response.Token);
        Assert.Equal(request.Email, response.User.Email);
        Assert.Equal(request.Username, response.User.Username);
        
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        Assert.NotNull(user);
        Assert.Equal("fakeHash", user.PasswordHash);
        Assert.Equal(request.Username, user.Username);
    }
    
    [Fact]
    public async Task RegisterAsync_RegisterWithExistingEmail_ShouldThrow()
    {
        var request = new RegisterRequest()
        {
            Email = "test@email.com",
            Username = "testUser",
            Password = "testPassword"
        };
        
        var existingUser = User.Create(username: "existingUser", email: request.Email, passwordHash: "fakeHash");
        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _authService.RegisterAsync(request));
    }
    
    [Theory]
    [InlineData("test@email.com", "", "testPassword", "fakeHash")]
    [InlineData("test@email.com", "ceciestunusernamebeaucouptroplongpouretrevalide", "testPassword", "fakeHash")]
    [InlineData("", "testUser", "testPassword", "fakeHash")]
    [InlineData("invalidemail", "testUser", "testPassword", "fakeHash")]
    [InlineData("test@email.com", "testUser", "testPassword", "")]
    public async Task RegisterAsync_RegisterWithInvalidCredentials_ShouldThrow(
        string email,
        string username, 
        string password, 
        string hash)
    {
        var request = new RegisterRequest()
        {
            Email = email,
            Username = username,
            Password = password
        };

        _passwordHasher.Setup(h => h.Hash(request.Password)).Returns(hash);
        
        await Assert.ThrowsAsync<ArgumentException>(async () => await _authService.RegisterAsync(request));
    }
    
    [Fact]
    public async Task LoginAsync_ShouldLoginUserAndReturnAuthResponse()
    {
        var request = new LoginRequest()
        {
            Email = "test@email.com",
            Password = "testPassword"
        };
        
        _passwordHasher.Setup(h => h.Verify(request.Password, "validHash")).Returns(true);
        _tokenService.Setup(ts => ts.GenerateToken(It.IsAny<User>())).Returns("fakeToken");
        
        var existingUser = User.Create(username: "existingUser", email: request.Email, passwordHash: "validHash");
        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync();
        
        var response = await _authService.LoginAsync(request);

        Assert.NotNull(response);
        Assert.Equal("fakeToken", response.Token);
        Assert.Equal(request.Email, response.User.Email);
        Assert.Equal(existingUser.Username, response.User.Username);
    }
    
    [Fact]
    public async Task LoginAsync_EmailNotFound_ShouldThrow()
    {
        var request = new LoginRequest()
        {
            Email = "test@email.com",
            Password = "testPassword"
        };
        
        await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _authService.LoginAsync(request));
    }
    
    [Fact]
    public async Task LoginAsync_WrongPassword_ShouldThrow()
    {
        var request = new LoginRequest()
        {
            Email = "test@email.com",
            Password = "testPassword"
        };
        
        _passwordHasher.Setup(h => h.Verify(request.Password, "validHash")).Returns(false);
         
        var existingUser = User.Create(username: "existingUser", email: request.Email, passwordHash: "validHash");
        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync();
        
        await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _authService.LoginAsync(request));
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}