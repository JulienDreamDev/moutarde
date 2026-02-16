using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using moutarde_back.Entities;
using moutarde_back.Infrastructure.Security;

namespace moutarde_tests;

public class TokenServiceTests
{
    private readonly ITokenService _tokenService;
    private const string Issuer = "test_issuer";
    private const string Audience = "test_audience";
    private const int ExpirationHours = 24;

    public TokenServiceTests()
    {
        var settings = new Dictionary<string, string>()
        {
            {"Jwt:Secret", "mwxcWDsZx8clMrVhYgSlIfO+LGbGufvE0KCYlvTsi8g="}, // random secret for testing
            {"Jwt:Issuer", Issuer},
            {"Jwt:Audience", Audience},
            {"Jwt:ExpirationHours", ExpirationHours.ToString()}
        };

        IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(settings!).Build();
        _tokenService = new JwtTokenService(configuration);
    }
    
    [Fact]
    public void GenerateToken_ShouldReturnToken()
    {
        var user = User.Create("testUser", "test@email.com", "testPasswordHash");
        var token = _tokenService.GenerateToken(user);
        
        Assert.False(string.IsNullOrWhiteSpace(token));
        Assert.Equal(3, token.Split('.').Length); // A JWT token should have three parts separated by dots
    }
    
    [Fact]
    public void GenerateToken_TokenShouldReturnValidUser()
    {
        var user = User.Create("testUser", "test@email.com", "testPasswordHash");
        var token = _tokenService.GenerateToken(user);
        
        var jwtToken =  new JwtSecurityTokenHandler().ReadJwtToken(token);
        
        var id = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        var username = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value;
        var email = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;
        
        Assert.Equal(user.Id.ToString(), id);
        Assert.Equal(user.Username, username);
        Assert.Equal(user.Email, email);
    }
    
    [Fact]
    public void GenerateToken_TokenShouldReturnValidAudienceAndIssuer()
    {
        var user = User.Create("testUser", "test@email.com", "testPasswordHash");
        var token = _tokenService.GenerateToken(user);
        
        var jwtToken =  new JwtSecurityTokenHandler().ReadJwtToken(token);
        
        var issuer = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Iss).Value;
        var audience = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Aud).Value;
        
        Assert.Equal(Issuer, issuer);
        Assert.Equal(Audience, audience);
    }
    
    [Fact]
    public void GenerateToken_TokenShouldReturnCorrectExpirationDate()
    {
        var user = User.Create("testUser", "test@email.com", "testPasswordHash");
        var token = _tokenService.GenerateToken(user);
        
        var jwtToken =  new JwtSecurityTokenHandler().ReadJwtToken(token);
        
        var exp = jwtToken.ValidTo;
        var expectedExp = DateTime.UtcNow.AddHours(ExpirationHours);
        
        var timeDifference = expectedExp - exp;
        
        Assert.True(timeDifference.Minutes < 1); // Allow a small time diff due to processing time
    }
    
    [Fact]
    public void GenerateToken_TwoTokensFromSameUserShouldBeDifferent()
    {
        var user = User.Create("testUser", "test@email.com", "testPasswordHash");
        var token1 = _tokenService.GenerateToken(user);
        var token2 = _tokenService.GenerateToken(user);
        
        Assert.NotEqual(token1, token2);
    }
}