using System.Numerics;
using NumberTheory.Extensions;

namespace CryptographyLib.Attacks;

public static class WienerAttack
{
    private static readonly BigInteger M = new(0x01010101);
    public static List<(BigInteger, BigInteger)> Attack(BigInteger isN, BigInteger isE) 
    {
        var limitD = isN.Sqrt().Sqrt() / 3;
        
        return BigIntegerExtensions
            .CFRAC(isE, isN)
            .GetConvergent()
            .Where(s =>
            {
                if (s.Item2 > limitD)
                    return false;
                var c = BigInteger
                    .ModPow(M, isE, isN);
                
                var m = BigInteger
                    .ModPow(c, s.Item2, isN);

                return M == m;

            })
            .ToList();
    }
}