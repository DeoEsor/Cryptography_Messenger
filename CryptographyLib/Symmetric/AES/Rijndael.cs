using CryptographyLib.KeyExpanders;

namespace CryptographyLib.Symmetric.AES;

public class Rijndael : SymmetricEncryptorBase
{
    public Rijndael(IExpandKey expandKey) : base(expandKey)
    {
    }

    public override byte[] Encrypt(byte[] value)
    {
        throw new NotImplementedException();
    }

    public override byte[] Decrypt(byte[] value)
    {
        throw new NotImplementedException();
    }
}