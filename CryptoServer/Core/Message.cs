using System.ComponentModel.DataAnnotations;

namespace CryptoServer.Core;

public class Message
{
    public enum MessageType
    {
        STRING,
        FILE
    }
    [Key]
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int ChatId { get; set; }
    
    public Files? File { get; set; }
    
    public Texts? Text { get; set; }
    
    public string? FileName { get; set; } = String.Empty;
    public MessageType Type { get; set; }
    
    public DateTime Created { get; set; } = DateTime.Now;
    
    public bool IsFile => Type == MessageType.FILE;
}