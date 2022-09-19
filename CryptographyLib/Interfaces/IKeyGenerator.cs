namespace CryptographyLib.Interfaces;

public interface IKeyGenerator<T>
{
    byte[] Generate(T[] values);
    
    Task<byte[]> GenerateAsync(T[] values);
}

public interface IKeyGenerator
{
    byte[] Generate(params object[] value);
    
    Task<byte[]> GenerateAsync(params object[] value);
}