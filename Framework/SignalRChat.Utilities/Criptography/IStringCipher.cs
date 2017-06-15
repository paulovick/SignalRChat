namespace SignalRChat.Utilities.Criptography
{
    public interface IStringCipher
    {
        string Encrypt(string plainText, string passPhrase = "passPhrase");
        string Decrypt(string cipherText, string passPhrase = "passPhrase");
    }
}
