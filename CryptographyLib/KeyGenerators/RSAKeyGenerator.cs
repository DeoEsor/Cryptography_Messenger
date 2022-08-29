using System.Collections;
using System.Numerics;
using CryptographyLib.Extensions;
using CryptographyLib.Interfaces;
using NumberTheory.Euclid;
using NumberTheory.PrimalCheckers;
using NumberTheory.RandomGenerators;
using NumberTheory.Symbols;

namespace CryptographyLib.KeyGenerators;

public sealed class RsaKeyGenerator : AsymmetricKeyGenerator
{
    private BigInteger _e;


    private Lazy<((BigInteger, BigInteger), (BigInteger, BigInteger))> _generatedPair;
    public BigInteger E
    {
        get => _e;
        set
        {
            _e = value;
            _generatedPair = new Lazy<((BigInteger, BigInteger), (BigInteger, BigInteger))>(Init);
        }
    }

    private PrimalRandomGenerator Generator { get; set; } 
    public RsaKeyGenerator(BigInteger e,
        Lazy<((BigInteger, BigInteger), (BigInteger, BigInteger))> generatedPair,
        PrimalRandomGenerator? generator = null!, 
        IKeyGenerator privateKeyGenerator = default!, 
        IKeyGenerator publicKeyGenerator = default!)
        : base(privateKeyGenerator, publicKeyGenerator)
    {
        E = e;
        _generatedPair = generatedPair;
        Generator = generator ?? new BruteForcePrimalRandomGenerator(new MillerRabinTest());
    }

    public override byte[] CreatePrivateKey(params object[] value)
        =>
            new BitArray(_generatedPair.Value.Item2.Item1.ToByteArray())
                .Concat(new BitArray(_generatedPair.Value.Item2.Item2.ToByteArray()))
                .ToBytes();

    public override byte[] CreatePublicKey(params object[] value)
        =>
            new BitArray(_generatedPair.Value.Item1.Item1.ToByteArray())
                .Concat(new BitArray(_generatedPair.Value.Item1.Item2.ToByteArray()))
                .ToBytes();

    ((BigInteger, BigInteger), (BigInteger, BigInteger)) Init()
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
        return ((_e,mod),(Euler.EulerFunc(_e),mod));
    }
    
}