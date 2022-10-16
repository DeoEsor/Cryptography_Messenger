// ReSharper disable NonReadonlyMemberInGetHashCode
namespace NumberTheory;

/// <summary>
///     GF ( 2^8 ), where 2 - field characteristics, 8 - field Module
/// </summary>
public sealed class GaloisField
{
    public  int Module { get; set; } = 256;
        
    //irreducible polynomial used : x^8 + x^4 + x^3 + x^2 + 1 (0x11D)
    private const int Polynomial = 0x11D;
        
    //generator to be used in Exp & Log table generation
    private const byte Generator = 0x2;

    private static readonly byte[]? Exp;

    private static readonly byte[]? Log;

    private byte Value { get; set; }

    public GaloisField() => Value = 0;

    public GaloisField(byte value) => Value = value;

    //operators
    public static explicit operator GaloisField(byte b)
    {
        var f = new GaloisField(b);
        return f;
    }

    public static explicit operator byte(GaloisField f) => f.Value;

    public static GaloisField operator+ (GaloisField a, GaloisField b) => new GaloisField((byte)(a.Value ^ b.Value));

    public static GaloisField operator- (GaloisField a, GaloisField b) => new GaloisField((byte)(a.Value ^ b.Value));

    public static GaloisField operator* (GaloisField a, GaloisField b)
    {
        var result = new GaloisField(0);

        if (a.Value == 0 || b.Value == 0) return result;
            

        var bres = (byte)((Log?[a.Value] + Log?[b.Value]) % (a.Module-1))!;
        bres = (byte)Exp?[bres]!;
        result.Value = bres;
        return result;
    }

    public static GaloisField operator/ (GaloisField a, GaloisField b)
    {
        if (b.Value == 0)
            throw new ArgumentException("Divisor cannot be 0", nameof(b));

        var result = new GaloisField(0);

        if (a.Value == 0) return result;
            
        var bres = (byte)((a.Module - 1 + Log[a.Value] - Log[b.Value]) % (a.Module-1));
        bres = Exp[bres];
        result.Value = bres;
            
        return result;
    }

    public static GaloisField Pow(GaloisField f, byte exp)
    {
        var fres = new GaloisField(1);
        for (byte i = 0; i < exp; i++)
            fres *= f;
            
        return fres;
    }

    public static bool operator== (GaloisField a, GaloisField b) => a.Value == b.Value;

    public static bool operator !=(GaloisField a, GaloisField b) => a.Value != b.Value;

    public override bool Equals(object? obj)
    {
        if (obj is not GaloisField field)
            return false;
            
        return Value == field.Value;
    }

    public IEnumerable<byte> IrredPolynomes()
    {
        var i = 0;
        for (ushort number = 0b100000000; number <= 0b111111111; number +=2) 
        {
            if (i == 30) yield break;
            var remnants_count = 0;

            for (byte j = 0b10; j < 0b100000; j++)
                if (Remnant(number, j) == 0)
                    break;
                else remnants_count++;

            if (remnants_count != 0) continue;
                
            yield return (byte)number;
            i++;
        }
    }
        
    public byte Remnant(ushort poly, ushort module)
    {
        var module_count = Degree(module);
        var dif = 0;

        while ((dif = Degree(poly)) - module_count >= 0)
            poly = (ushort)(poly ^ module << dif);
        
        return (byte)poly;
    }
        
    private int Degree(ushort poly)
    {
        var res=0;

        for (; poly > 0; poly >>= 0b1)  
            if ((poly & 0b1) == 0b1) res++;
        return res;
    }

    public override int GetHashCode() => Value;

    public override string ToString() => Value.ToString();

    /// <summary>
    /// Multiplication method which is only used in Exp & Log table generation
    /// Implemented with Russian Peasant Multiplication algorithm
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static byte Multiply(byte a, byte b) 
    {
        byte result = 0;
        var aa = a;
        var bb = b;
        while (bb != 0)
        {
            if ((bb & 1) != 0)
                result ^= aa;
                
            var highestBit = (byte)(aa & 0x80);
                
            aa <<= 1;
                
            if (highestBit != 0)
                aa ^= (Polynomial & 0xFF);
                
            bb >>= 1;
        }
        return result;
    }
    
    public static byte MultGf256(byte a, byte b, byte f)
    {
        byte t = 0, mask = 1;

        for (var i = 0; i < 8; i++)
        {
            if ((b & mask) != 0)
                t = (byte)(t ^ a);

            a = (a & 128) == 128 ? (byte)(a << 1) : (byte)((a << 1) ^ f);

            mask = (byte)(mask << 1);
        }
        return t;
    }

    public static byte PowerGf256(byte a, byte b, byte f)
    {
        byte t = 1;

        while (b > 0)
        {
            if (b % 2 == 1)
                t = MultGf256(a, b, f);
            a = MultGf256(a, a, f);
            b = (byte)(b >> 1);
        }
        return t;
    }
}