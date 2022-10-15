using CryptographyLib.Symmetric.FeistelNetwork;

namespace CryptographyLib.Tests.SPNetwork;

[TestFixture]
public class PBlockTest
{
    private PBlock pBlock = new PBlock();
        
    int testPvalue = 50;
    private byte[] test = new byte[]{ 0, 1, 2, 3, 4, 5} ;  
    byte[] testPBlock = new byte[] { 5,3,6,1, 4,2 };
    
    [Test]
    public void PBlockEncryptionInt32()
    {
        var encrypted = pBlock.Encrypt(testPvalue, testPBlock);

        Assert.AreEqual(37, BitConverter.ToInt32(encrypted));
    }
        
    [Test]
    public void PBlockDecryption()
    {
        var encrypted = pBlock.Encrypt(BitConverter.GetBytes(testPvalue), testPBlock);
        var decrypted = pBlock.Decrypt(encrypted, testPBlock);

        Assert.AreEqual(50, BitConverter.ToInt32(decrypted));
    }
}