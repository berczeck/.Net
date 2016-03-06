namespace Framework.Encriptacion
{
    public interface IEncriptionProvider
    {
        string EncriptarTexto(string plainText, string encryptionPrivateKey = null);
        string DesencriptarTexto(string cipherText, string encryptionPrivateKey = null);
    }
}
