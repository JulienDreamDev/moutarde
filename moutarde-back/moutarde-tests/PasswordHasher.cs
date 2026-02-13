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
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Hash_PasswordEmpty_ShouldThrow(string password)
    {
        Assert.Throws<ArgumentException>(() => _hasher.Hash(password));
    }
    
    [Fact]
    public void Hash_TwoSamePasswordShouldNotHaveSameHash()
    {
        const string password1 = "password123";
        const string password2 = "password123";
        var hash1 = _hasher.Hash(password1);
        var hash2 = _hasher.Hash(password2);
        
        Assert.NotEqual(hash1, hash2);
    }
    
    [Fact]
    public void Hash_TwoDifferentPasswordShouldNotHaveSameHash()
    {
        const string password1 = "password123";
        const string password2 = "anotherpassword";
        var hash1 = _hasher.Hash(password1);
        var hash2 = _hasher.Hash(password2);

        Assert.NotEqual(hash1, hash2);
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
    
    [Theory]
    [InlineData("", "somehash")]
    [InlineData("   ", "somehash")]
    [InlineData("somepassword", "")]
    [InlineData("somepassword", "  ")]
    public void Verify_PasswordOrHashEmpty_ShouldThrow(string password, string hash)
    {
        Assert.Throws<ArgumentException>(() => _hasher.Verify(password, hash));
    }
}