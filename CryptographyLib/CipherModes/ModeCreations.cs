using CryptographyLib.CipherModes.Realization;
using CryptographyLib.Interfaces;

namespace CryptographyLib.CipherModes;

public static partial class CipherMode
{
    private static CipherModeBase Cbc(ISymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountCbc || values[1] is not ulong ivCbc)
            throw new ArgumentException("Failed to read params");

        return new CBC(encryptor, ivCbc, blocksCountCbc);
    }

    private static CipherModeBase Ecb(ISymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb)
            throw new ArgumentException("Failed to read params");

        return new ECB(encryptor, blocksCountEcb);
    }

    private static CipherModeBase Cfb(ISymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountCfb || values[1] is not int ivCfb || values[2] is not int l)
            throw new ArgumentException("Failed to read params");

        return new CFB(encryptor, ivCfb, blocksCountCfb);
    }

    private static CipherModeBase Ofb(ISymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb || values[1] is not int ivOfb)
            throw new ArgumentException("Failed to read params");

        return new OFB(encryptor, ivOfb, blocksCountEcb);
    }

    private static CipherModeBase Ctr(ISymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb || values[1] is not int ivCtr)
            throw new ArgumentException("Failed to read params");

        return new CTR(encryptor, ivCtr, i=> i, blocksCountEcb);
    }

    private static CipherModeBase Rd(ISymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb || values[1] is not int ivRd)
            throw new ArgumentException("Failed to read params");

        return new RD(encryptor, ivRd, blocksCountEcb);
    }

    private static CipherModeBase Rdh(ISymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb || values[1] is not int ivRdh || values[2] is not byte[] hash)
            throw new ArgumentException("Failed to read params");

        return new RDH(encryptor, ivRdh, hash,blocksCountEcb);
    }
    
    private static CipherModeBase Cbc(IAsymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountCbc || values[1] is not ulong ivCbc)
            throw new ArgumentException("Failed to read params");

        return new CBC(encryptor, ivCbc, blocksCountCbc);
    }

    private static CipherModeBase Ecb(IAsymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb)
            throw new ArgumentException("Failed to read params");

        return new ECB(encryptor, blocksCountEcb);
    }

    private static CipherModeBase Cfb(IAsymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountCfb || values[1] is not int ivCfb || values[2] is not int l)
            throw new ArgumentException("Failed to read params");

        return new CFB(encryptor, ivCfb, blocksCountCfb);
    }

    private static CipherModeBase Ofb(IAsymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb || values[1] is not int ivOfb)
            throw new ArgumentException("Failed to read params");

        return new OFB(encryptor, ivOfb, blocksCountEcb);
    }

    private static CipherModeBase Ctr(IAsymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb || values[1] is not int ivCtr)
            throw new ArgumentException("Failed to read params");

        return new CTR(encryptor, ivCtr, i=> i, blocksCountEcb);
    }

    private static CipherModeBase Rd(IAsymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb || values[1] is not int ivRd)
            throw new ArgumentException("Failed to read params");

        return new RD(encryptor, ivRd, blocksCountEcb);
    }

    private static CipherModeBase Rdh(IAsymmetricEncryptor encryptor, object[] values)
    {
        if (values[0] is not int blocksCountEcb || values[1] is not int ivRdh || values[2] is not byte[] hash)
            throw new ArgumentException("Failed to read params");

        return new RDH(encryptor, ivRdh, hash,blocksCountEcb);
    }
}