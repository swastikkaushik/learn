using System.Security.Cryptography;
using System.Text;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }

    public bool Verify(string value, string hashedValue)
        => Hash(value) == hashedValue;
}
