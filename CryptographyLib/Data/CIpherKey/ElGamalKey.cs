using System.Numerics;

namespace CryptographyLib.Data.CIpherKey;

public record struct ElGamalKey
{
    /// <summary>
    /// Part of public ElGamal's key 
    /// </summary>
    public BigInteger Y { get; init; }
    /// <summary>
    /// Part of public ElGamal's key 
    /// </summary>
    public BigInteger G { get; init; }
    /// <summary>
    /// Part of public ElGamal's key 
    /// </summary>
    public BigInteger P { get; init; }
    /// <summary>
    /// Private ElGamal's key 
    /// </summary>
    public BigInteger X { get; set; }
}