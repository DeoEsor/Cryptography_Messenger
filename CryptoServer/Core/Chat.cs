using System.ComponentModel.DataAnnotations;

namespace CryptoServer.Core;

public class Chat
{
    [Key]
    public int Id { get; set; }
    public int ChatId { get; set; }
    public int UserId { get; set; }
    public byte[] KeyData { get; set; }
}