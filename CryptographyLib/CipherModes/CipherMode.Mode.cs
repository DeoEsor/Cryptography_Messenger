namespace CryptographyLib.CipherModes;

public static partial class CipherMode
{
    public enum Mode : byte
    {
        ECB,  
        CBC, 
        CFB, 
        OFB, 
        CTR, 
        RD, 
        RDH
    }
}