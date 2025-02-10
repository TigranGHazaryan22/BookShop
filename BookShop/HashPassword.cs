using System.Security.Cryptography;
using System.Text;

namespace BookShop
{
    public class HashPassword
    {
        public static string ProceedData(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashbytes = sha256.ComputeHash(inputBytes);

                string hashedPassword = Convert.ToHexString(hashbytes);
                password = hashedPassword;
            }

            return password;
        }
    }
}
