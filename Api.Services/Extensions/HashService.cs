using System;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services.Extencion;

public class HashService : IHashService
    {
        // Parámetros configurables
        private const int SaltSize = 16;        // 128-bit salt
        private const int HashSize = 32;        // 256-bit derived key
        private const int Iterations = 100_000; // recomendado mínimo (ajustar según necesidad)
        private const string FormatVersion = "v1";

        public string Hash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            // Generar salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Derivar la clave
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Construir cadena: versión$iter$saltBase64$hashBase64
            string saltB64 = Convert.ToBase64String(salt);
            string hashB64 = Convert.ToBase64String(hash);
            var stored = $"{FormatVersion}${Iterations}${saltB64}${hashB64}";
            return stored;
        }

        public bool Verify(string password, string storedHash)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(storedHash)) return false;

            // Parsear formato esperado
            // formato: v1$iterations$saltBase64$hashBase64
            var parts = storedHash.Split('$', 4);
            if (parts.Length != 4) return false;
            var version = parts[0];
            if (version != FormatVersion) return false; // solo manejamos v1 por ahora

            if (!int.TryParse(parts[1], out int iterations)) return false;
            var salt = Convert.FromBase64String(parts[2]);
            var expectedHash = Convert.FromBase64String(parts[3]);

            // Derivar con los mismos parámetros
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var actualHash = pbkdf2.GetBytes(expectedHash.Length);

            // Comparación en tiempo constante
            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }