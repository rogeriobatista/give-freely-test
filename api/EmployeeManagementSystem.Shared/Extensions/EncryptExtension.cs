using System.Text;
using XSystem.Security.Cryptography;

namespace EmployeeManagementSystem.Shared.Extensions
{
    public static class EncryptExtension
    {
        public static string Sha256(this string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
