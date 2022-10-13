using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoServer.Core;

[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }
    
    public string Username { get; set; }
    public byte[] Password { get; set; }

    public bool IsOnline { get; set; }
    
    public string? Color { get; set; }
    
    public string? ImageSource { get; set; }
    
    public string? Status { get; set; }
}