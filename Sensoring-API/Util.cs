using System.Security.Cryptography;
using System.Text;

namespace Sensoring_API;

/// <summary>
/// Provides utility methods for common operations.
/// </summary>
public static class Util
{
    /// <summary>
    /// Generates a SHA256 hash for the given input string.
    /// </summary>
    /// <param name="input">The input string to be hashed.</param>
    /// <returns>The hashed string.</returns>
    public static string HashWithSha256(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}