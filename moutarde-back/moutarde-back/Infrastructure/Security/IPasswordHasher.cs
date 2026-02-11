namespace moutarde_back.Infrastructure.Security;

public interface IPasswordHasher
{
    public static abstract string Hash(string password);
    public static abstract bool Verify(string password, string hash);
}