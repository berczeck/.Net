using System.Security.Cryptography;

namespace Framework.Encriptacion
{
    public class TripleDesEncriptionProvider : EncriptionProvider, IEncriptionProvider
    {
        public TripleDesEncriptionProvider()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptionKey">llave privada de tamaño mayor a 16</param>
        public TripleDesEncriptionProvider(string encryptionKey)
            : base(encryptionKey)
        {
        }

        protected override SymmetricAlgorithm CrearAlgoritmo()
        {
            return new TripleDESCryptoServiceProvider();
        }

        protected override int LongitudClave()
        {
            return 8;
        }
    }
}
