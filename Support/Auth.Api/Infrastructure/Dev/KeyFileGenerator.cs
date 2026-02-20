using System.Security.Cryptography;

namespace Auth.Api.Infrastructure.Dev
{
    public static class KeyFileGenerator
    {
        public static void Generate()
        {
            using var rsa = RSA.Create(2048);

            var pvKey = rsa.ExportRSAPrivateKeyPem();
            var pubKey = rsa.ExportRSAPublicKeyPem();


            var dir = Path.Combine("Infrastructure", "Dev");
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);

            File.WriteAllText(Path.Combine(dir, "private.pem"), pvKey);

            File.WriteAllText(Path.Combine(dir, "public.pem"), pubKey);

        }
    }
}
