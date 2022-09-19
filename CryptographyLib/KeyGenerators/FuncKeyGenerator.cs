using CryptographyLib.Interfaces;

namespace CryptographyLib.KeyGenerators;

public class FuncKeyGenerator<T> : IKeyGenerator<T> 
{
    public Func<T,byte[]> Func { get; set; } 
        
    public FuncKeyGenerator(Func<T,byte[]> func)
    {
        Func = func;
    }
     
    public byte[] Generate(T[] values)
    => values
                .Select(value => Func?.Invoke(value))
                .TakeWhile(temp => temp != null)
                .Cast<byte[]>()
                .ToList()
                .SelectMany( s=> s).ToArray();

    public Task<byte[]> GenerateAsync(T[] values)
        => Task.Run(() =>values
            .Select(value => Func?.Invoke(value))
            .TakeWhile(temp => temp != null)
            .Cast<byte[]>()
            .ToList()
            .SelectMany(s => s).ToArray());
}