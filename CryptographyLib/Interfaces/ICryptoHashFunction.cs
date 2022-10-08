namespace CryptographyLib.Interfaces;

public interface ICryptoHashFunction
{
    public int BlockSize { get;  }
    public byte[] CreateHash();
}