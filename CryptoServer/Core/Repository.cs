using Crypto.Infrastructure.Abstractions;
using CryptoServer.DBContexts;

namespace CryptoServer.Core;

public class Repository : IRepository<User>, IRepository<Chat>, IRepository<Message>
{

    public User ChangeStatus(string username, byte[] password, bool isOnlineNow)
    {
        using var a = new UsersDb();

        var user = a.Users.FirstOrDefault(user => user.Username == username && user.Password == password);
        if (user == null) return null!;
        user.IsOnline = isOnlineNow;
        a.SaveChanges();
        return user;
    }
    
    List<User> IRepository<User>.GetDataList()
    {
        using var a = new UsersDb();

        return new List<User>(a.Users);
    }

    Message IRepository<Message>.GetData(int id)
    {
        using var a = new UsersDb();

        return a.Messages.FirstOrDefault(mes=> mes.Id==id);
    }

    public IRepository<Message> AddData(Message data)
    {
        using var a = new UsersDb();

        a.Messages.Add(data);
        a.SaveChanges();
        return this;
    }

    public IRepository<Message> AddDataList(IList<Message> data)
    {
        using var a = new UsersDb();

        a.Messages.AddRange(data);
        a.SaveChanges();
        return this;
    }

    List<Message> IRepository<Message>.GetDataList()
    {
        using var a = new UsersDb();

        return new List<Message>(a.Messages);
    }

    Chat IRepository<Chat>.GetData(int id)
    {
        using var a = new UsersDb();

        return a.Chats.FirstOrDefault(s => s.Id == id);
    }

    public IRepository<Chat> AddData(Chat data)
    {
        using var a = new UsersDb();

        a.Chats.Add(data);
        a.SaveChanges();
        return this;
    }

    public IRepository<Chat> AddDataList(IList<Chat> data)
    {
        using var a = new UsersDb();

        a.Chats.AddRange(data);
        a.SaveChanges();
        return this;
    }

    List<Chat> IRepository<Chat>.GetDataList()
    {
        using var a = new UsersDb();

        return new List<Chat>(a.Chats);
    }

    User IRepository<User>.GetData(int id)
    {
        using var a = new UsersDb();

        return a.Users.FirstOrDefault(u => u.Id == id);
    }

    public IRepository<User> AddData(User data)
    {
        using var a = new UsersDb();

        a.Users.Add(data);
        a.SaveChanges();
        return this;
    }

    public IRepository<User> AddDataList(IList<User> data)
    {
        using var a = new UsersDb();

        a.Users.AddRange(data);
        a.SaveChanges();
        return this;
    }
}