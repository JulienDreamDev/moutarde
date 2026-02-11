using moutarde_back.Infrastructure.Security;

namespace moutarde_tests;

public class UnitTest1
{
    [Fact]
    public void PasswordHasBeenHashedTest()
    {
        IPasswordHasher passwordHasher = new BCryptPasswordHasher();
        string password = "password123";
        string hash = passwordHasher.Hash(password);

        Assert.NotEqual(password, hash);
    }
    
    [Fact]
    public void PasswordVerificationTest()
    {
        IPasswordHasher passwordHasher = new BCryptPasswordHasher();
        string password = "password123";
        string hash = passwordHasher.Hash(password);

        Assert.True(passwordHasher.Verify(password, hash));
    }
    
    [Fact]
    public void PasswordVerificationFailedTest()
    {
        IPasswordHasher passwordHasher = new BCryptPasswordHasher();
        string password = "password123";
        string wrongPassword = "wrongpassword";
        string hash = passwordHasher.Hash(password);

        Assert.False(passwordHasher.Verify(wrongPassword, hash));
    }
}