namespace CryptographyLib.Data;

public struct CipherResult<T>
{
    public CipherResult()
    {
        Value = default;
    }

    public bool IsSuccessful => Exception == null;

    public Exception? Exception { get; set; } = null;
    public T? Value { get; set; }

}