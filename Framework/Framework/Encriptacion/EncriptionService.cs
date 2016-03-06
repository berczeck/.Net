using System;
using System.Security.Cryptography;
using System.Text;
using CuttingEdge.Conditions;
using Framework.Validacion;

namespace Framework.Encriptacion
{
    public class EncriptionService
    {
        /// <summary>
        /// Genera una clave aleatoria indicando la longitud del valor generado
        /// </summary>
        /// <param name="size">tamaño</param>
        /// <returns>Una clave nueva</returns>
        public static string GenerarClaveAleatoria(int size = 16)
        {
            // Genera un numero aleatoria encriptado
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buff = new byte[size];
                rng.GetBytes(buff);

                // Retorna una cadena en Base64 del numero aleatorio
                return Convert.ToBase64String(buff);
            }
        }
        
        /// <summary>
        /// Genera el valor hash de un texto
        /// </summary>
        /// <param name="password">texto</param>
        /// <param name="encryptionPrivateKey">llave privada de tamaño 16</param>
        /// <returns>Valor hash</returns>
        public static string GenerarHash(string password, string encryptionPrivateKey)
        {
            Condicion.ValidarParametro(encryptionPrivateKey)
                .IsNotNullOrWhiteSpace(Condicion.MensajeValorNulo)
                .Evaluate(x => x.Length >= 16, "La longitud de la clave es incorrecta. Debe ser una calve de 16 digitos.");
            
            byte[] secretBytes = Encoding.ASCII.GetBytes(encryptionPrivateKey);
            using (var hmac = new HMACSHA256(secretBytes))
            {
                byte[] dataBytes = Encoding.ASCII.GetBytes(password);
                byte[] computedHash = hmac.ComputeHash(dataBytes);

                return Convert.ToBase64String(computedHash);
            }
        }
    }
}