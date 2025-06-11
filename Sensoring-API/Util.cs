using System.Security.Cryptography;
using System.Text;

namespace Sensoring_API;

public static class Util
{
    public static string HashWithSha256(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}