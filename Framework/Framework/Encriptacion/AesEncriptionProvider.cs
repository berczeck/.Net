using System.Security.Cryptography;

namespace Framework.Encriptacion
{
    public class AesEncriptionProvider : EncriptionProvider, IEncriptionProvider
    {
        public AesEncriptionProvider()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptionKey">llave privada de tamaño mayor a 32</param>
        public AesEncriptionProvider(string encryptionKey) : base(encryptionKey)
        {
        }

        protected override SymmetricAlgorithm CrearAlgoritmo()
        {
            return new AesCryptoServiceProvider();
        }

        protected override int LongitudClave()
        {
            return 16;
        }
    }
}
