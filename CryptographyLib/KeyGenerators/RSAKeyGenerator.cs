using System.Numerics;
using CryptographyLib.Data;
using CryptographyLib.Interfaces;
using NumberTheory.Euclid;
using NumberTheory.PrimalCheckers;
using NumberTheory.RandomGenerators;
using NumberTheory.Symbols;

namespace CryptographyLib.KeyGenerators;

public sealed class RSAKeyGenerator : AsymmetricKeyGenerator
{
    private BigInteger _e;


    private Lazy<((BigInteger, BigInteger), (BigInteger, BigInteger))> GeneratedPair;
    
    public BigInteger E
    {
        get => _e;
        set
        {
            _e = value;
            GeneratedPair = new Lazy<((BigInteger, BigInteger), (BigInteger, BigInteger))>(Init);
        }
    }

    private PrimalRandomGenerator Generator { get; set; } 
    
    public RSAKeyGenerator(BigInteger e,PrimalRandomGenerator generator = null!, 
        IKeyGenerator privateKeyGenerator = default!, 
        IKeyGenerator publicKeyGenerator = default!)
    {
        E = e;
        Generator = generator ?? new BruteForcePrimalRandomGenerator(new MillerRabinTest());
    }

    public override Key GenerateKeys() => Key.CreateAsymmetricKey(CreatePrivateKey(), CreatePrivateKey());

    private byte[] CreatePrivateKey(params object[] value)
    {
        var res = new List<byte>();
        var p = GeneratedPair.Value.Item2.Item1.ToByteArray();
        var q = GeneratedPair.Value.Item2.Item2.ToByteArray();
        res.AddRange(BitConverter.GetBytes(p.Length));
        res.AddRange(p);
        res.AddRange(BitConverter.GetBytes(q.Length));
        res.AddRange(q);
        return res.ToArray();
    }


    private byte[] CreatePublicKey(params object[] value)
    {
        var res = new List<byte>();
        var p = GeneratedPair.Value.Item1.Item1.ToByteArray();
        var q = GeneratedPair.Value.Item1.Item2.ToByteArray();
        res.AddRange(BitConverter.GetBytes(p.Length));
        res.AddRange(p);
        res.AddRange(BitConverter.GetBytes(q.Length));
        res.AddRange(q);
        return res.ToArray();
    }

    private ((BigInteger, BigInteger), (BigInteger, BigInteger)) Init()
    {
        Random random = new Random();
        var p = Generator.Generate();
        var q = Generator.Generate();
        var mod = p * q;
        var phi = (p-1) * (q  - 1);
        if (ExtendedGCD.Solve(E, phi, out var _, out var _) == 1)
            while (true)
            {
                _e = random.Next(1, (int)(phi - 1));
                var gcd = ExtendedGCD.Solve(E, phi, out var _, out var _);
                if (gcd == 1)
                    break;
            }
        return ((_e,mod),(Euler.EulerFunc((long)_e),mod));
    }
    
}