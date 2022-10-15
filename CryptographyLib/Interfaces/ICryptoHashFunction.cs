namespace CryptographyLib.Interfaces;

public interface ICryptoHashFunction
{
    
    /// <summary>
    /// Method that receiving the message and returns hash of the message
    /// </summary>
    public byte[] CreateHash(byte[] mes);
}