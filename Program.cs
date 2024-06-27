using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public static string CreateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    public static string HashPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] combinedBytes = Encoding.UTF8.GetBytes(salt + password);
            byte[] hashBytes = sha256.ComputeHash(combinedBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}

public class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Passwort eingeben: ");
            string password = Console.ReadLine();
            string salt = PasswordHasher.CreateSalt();
            string hashedPassword = PasswordHasher.HashPassword(password, salt);

            Console.WriteLine($"Salt: {salt}");
            Console.WriteLine($"Hashed Password: {hashedPassword}");
            Console.ReadKey();
        }
    }
}