using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using CuttingEdge.Conditions;
using Framework.Validacion;

namespace Framework.Encriptacion
{
    public abstract class EncriptionProvider
    {
        protected abstract SymmetricAlgorithm CrearAlgoritmo();
        
        private readonly string _encryptionKey;
        protected abstract int LongitudClave();

        protected EncriptionProvider()
        {
        }

        protected void ValidarClave(string key)
        {
            int longitudClave = LongitudClave();

            Condicion.ValidarParametro(key)
                .IsNotNullOrWhiteSpace(Condicion.MensajeValorNulo)
                .Evaluate(x => x.Length >= longitudClave, string.Format("La longitud de la clave es incorrecta. Debe ser una clave de {0} digitos.",longitudClave));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptionKey">llave privada</param>
        protected EncriptionProvider(string encryptionKey)
        {
            ValidarClave(encryptionKey);
            _encryptionKey = encryptionKey;
        }

        /// <summary>
        /// Encripta una cadena usando el algoritmo TripleDes
        /// </summary>
        /// <param name="plainText">texto a encriptar</param>
        /// <param name="encryptionPrivateKey">llave privada</param>
        /// <returns>una cadena encriptada</returns>
        public string EncriptarTexto(string plainText, string encryptionPrivateKey = null)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return plainText;
            }

            int longitudClave = LongitudClave();

            if (string.IsNullOrWhiteSpace(encryptionPrivateKey))
            {
                encryptionPrivateKey = _encryptionKey;
            }
            
            ValidarClave(encryptionPrivateKey);

            using (var tDeSalg = CrearAlgoritmo())
            {
                tDeSalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
                tDeSalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, longitudClave));

                byte[] encryptedBinary = EncriptarTextoEnMemoria(plainText, tDeSalg.Key, tDeSalg.IV);
                return Convert.ToBase64String(encryptedBinary);
            }

        }

        /// <summary>
        /// Desencripta una cadena usando el algoritmo TripleDes
        /// </summary>
        /// <param name="cipherText">texto a desencriptar</param>
        /// <param name="encryptionPrivateKey">llave privada de tamaño 16</param>
        /// <returns>una cadena desencriptada</returns>
        public string DesencriptarTexto(string cipherText, string encryptionPrivateKey = null)
        {
            if (String.IsNullOrEmpty(cipherText))
            {
                return cipherText;
            }

            if (string.IsNullOrWhiteSpace(encryptionPrivateKey))
            {
                encryptionPrivateKey = _encryptionKey;
            }
            
            ValidarClave(encryptionPrivateKey);

            int longitudClave = LongitudClave();

            using (var tDeSalg = CrearAlgoritmo())
            {
                tDeSalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
                tDeSalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, longitudClave));

                byte[] buffer = Convert.FromBase64String(cipherText);
                return DesencriptarTextoDesdeMemoria(buffer, tDeSalg.Key, tDeSalg.IV);
            }
        }

        protected byte[] EncriptarTextoEnMemoria(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (
                    var cs = new CryptoStream(ms, CrearAlgoritmo().CreateEncryptor(key, iv),
                        CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        protected string DesencriptarTextoDesdeMemoria(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (
                    var cs = new CryptoStream(ms, CrearAlgoritmo().CreateDecryptor(key, iv),
                        CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs, new UnicodeEncoding()))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
