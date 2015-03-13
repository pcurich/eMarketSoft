namespace Soft.Services.Security
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Crea el random
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        string CreateSaltKey(int size);

        /// <summary>
        /// Crea la clave hash0
        /// </summary>
        /// <param name="password"></param>
        /// <param name="saltkey"></param>
        /// <param name="passwordFormat"></param>
        /// <returns></returns>
        string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1");

        /// <summary>
        /// Texto encriptado
        /// </summary>
        /// <param name="plainText">Texto a encriptar</param>
        /// <param name="encryptionPrivateKey">Llave provada para encriptar</param>
        /// <returns>Texto encriptado</returns>
        string EncryptText(string plainText, string encryptionPrivateKey = "");

        /// <summary>
        /// Descencriptar texto
        /// </summary>
        /// <param name="cipherText">Texto a desencriptar</param>
        /// <param name="encryptionPrivateKey">llave privada para desencriptar</param>
        /// <returns>Descencriptar texto</returns>
        string DecryptText(string cipherText, string encryptionPrivateKey = "");

    }
}