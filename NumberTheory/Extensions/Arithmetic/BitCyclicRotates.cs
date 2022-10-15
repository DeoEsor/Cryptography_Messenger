namespace NumberTheory.Extensions.Arithmetic;

public static class BitCyclicRotates
{
    public static int ShiftRotateLeft(this int value, int shift = 1)
    {
        if (shift == 0) return value;
    
        var right = value >> 1;
        
        if (32 - shift <= 1) 
            return value << shift | right;
        
        right &= 0x7FFFFFFF;
        right >>= 32 - shift - 1;
        
        return value << shift | right;
    }
    
    public static uint ShiftRotateLeft(this uint value, int shift = 1)
    {
        if (shift == 0) return value;
    
        var right = value >> 1;
        
        if (32 - shift <= 1) 
            return value << shift | right;
        
        right &= 0x7FFFFFFF;
        right >>= 32 - shift - 1;
        
        return value << shift | right;
    }

    public static int ShiftRotateRight(this int value, int shift = 1)
    {
        if (shift == 0) return value;
        var v = (uint)value;
        var right = v >> 1;
        
        if (shift <= 1)
            return (int)((v << (32 - shift)) | right);
        
        right &= 0x7FFFFFFF;
        right >>= shift - 1;
        
        return (int)((v << (32 - shift)) | right);
    }
    
    public static uint ShiftRotateRight(this uint value, int shift  = 1)
    {
        if (shift == 0) return value;
        var right = value >> 1;
        
        if (shift <= 1)
            return (value << (32 - shift)) | right;
        
        right &= 0x7FFFFFFF;
        right >>= shift - 1;
        
        return (value << (32 - shift)) | right;
    }
}