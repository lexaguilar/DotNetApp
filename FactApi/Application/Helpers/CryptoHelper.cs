
using System.Security.Cryptography;
using System.Text;

namespace FactApi.Application.Helpers;

internal static class CryptoHelper
{
    const int SaltLength = 8;
    internal const string ApiKey = "API_KEY";
    public static bool ComparePassword(string pwd, string pwdHash, string salt)
    {
        using (var sha = SHA256.Create())
        {
            var bytes = sha.ComputeHash(Encoding.ASCII.GetBytes(pwd + salt));
            var hash = BitConverter.ToString(bytes).Replace("-", null);
            return String.Compare(pwdHash, hash, true) == 0;
        }
    }

    public static (string PasswordHash, string Salt) ComputePassword(string pwd)
    {
        using (var sha = SHA256.Create())
        {
            var saltBytes = RandomNumberGenerator.GetBytes(SaltLength);
            string salt = Convert.ToBase64String(saltBytes);
            var bytes = sha.ComputeHash(Encoding.ASCII.GetBytes(pwd + salt));
            var hash = BitConverter.ToString(bytes).Replace("-", null);
            return (hash, salt);
        }
    }

    public static string GetRndPwd()
    {
        var d = RandomNumberGenerator.GetBytes(12);
        return Convert.ToBase64String(d);
    }

}