using CryptographyLib.Extensions;
using CryptographyLib.Symmetric.FeistelNetwork;

namespace CryptographyLib.Tests.SPNetwork;

[TestFixture]
public class SBlockTest
{
    private SBlock sBlock = new SBlock();
        
    byte testvalue = 0;

    private Func<byte, byte> SBlockRule = (b => (byte)((3 * b + 7) % 16));
    private byte[] SBlock = new byte[] { 7,10,13,0,3,6,9,12,15,2,5,8,11,14,1,4};


    [Test, Timeout(2000)]
    public void SBlockEncryption()
    {
        var res = sBlock.Encrypt(new byte[]{0,1,2}, SBlock.ConvertToDict(), 8);    
        
        Assert.AreEqual(7, res[0]);
        Assert.AreEqual(10, res[1]);
        Assert.AreEqual(13, res[2]);
    }
    
    [Test, Timeout(2000)]
    public void SBlockDecryption()
    {
        Assert.True(true);
        var res = sBlock.Decrypt(new byte[]{7,10,13}, SBlock.ConvertToDict(), 8);
        Assert.AreEqual(0, res[0]);
        Assert.AreEqual(1, res[1]);
        Assert.AreEqual(2, res[2]);
    }
}