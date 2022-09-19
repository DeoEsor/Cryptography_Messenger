using Crypto.Domain.Data;

namespace Crypto.Domain.Models;

public class Chat
{
	public int Id { get; set; }
	public List<User> Users { get; set; } = new List<User>();
	public Key Key { get; set; } = null!;
	public List<Guid> Messages { get; set; } = new List<Guid>();
}