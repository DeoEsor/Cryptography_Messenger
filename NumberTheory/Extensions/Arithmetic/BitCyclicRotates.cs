namespace NumberTheory.Extensions.Arithmetic;

public static class BitCyclicRotates
{
    public static int ShiftRotateLeft(this int value, int bits)
    {
        if (bits == 0) return value;
    
        var right = value >> 1;
        
        if ((32 - bits) <= 1) 
            return value << bits | right;
        
        right &= 0x7FFFFFFF;
        right >>= 32 - bits - 1;
        
        return value << bits | right;
    }
    
    public static uint ShiftRotateLeft(this uint value, int bits)
    {
        if (bits == 0) return value;
    
        var right = value >> 1;
        
        if ((32 - bits) <= 1) 
            return value << bits | right;
        
        right &= 0x7FFFFFFF;
        right >>= 32 - bits - 1;
        
        return value << bits | right;
    }

    public static int ShiftRotateRight(this int value, int bits)
    {
        if (bits == 0) return value;
        var v = (uint)value;
        var right = v >> 1;
        
        if (bits <= 1)
            return (int)((v << (32 - bits)) | right);
        
        right &= 0x7FFFFFFF;
        right >>= bits - 1;
        
        return (int)((v << (32 - bits)) | right);
    }
    
    public static uint ShiftRotateRight(this uint value, int bits)
    {
        if (bits == 0) return value;
        var right = value >> 1;
        
        if (bits <= 1)
            return (value << (32 - bits)) | right;
        
        right &= 0x7FFFFFFF;
        right >>= bits - 1;
        
        return (value << (32 - bits)) | right;
    }
}