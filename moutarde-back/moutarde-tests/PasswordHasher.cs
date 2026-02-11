using moutarde_back.Infrastructure.Security;

namespace moutarde_tests;

public class PasswordHasher
{
    private readonly IPasswordHasher _hasher = new BCryptPasswordHasher();
    
    [Fact]
    public void Hash_PasswordHasBeenHashed()
    {
        const string password = "password123";
        var hash = _hasher.Hash(password);

        Assert.NotEqual(password, hash);
    }
    
    [Fact]
    public void Verify_PasswordMatchHash()
    {
        const string password = "password123";
        var hash = _hasher.Hash(password);

        Assert.True(_hasher.Verify(password, hash));
    }
    
    [Fact]
    public void Verify_PasswordShouldNotMatchHash()
    {
        const string password = "password123";
        const string wrongPassword = "wrongpassword";
        var hash = _hasher.Hash(password);

        Assert.False(_hasher.Verify(wrongPassword, hash));
    }
    
    [Fact]
    public void Verify_InvalidHash()
    {
        const string password = "password123";
        const string hash = "invalidhash";
        
        Assert.False(_hasher.Verify(password, hash));
    }
}