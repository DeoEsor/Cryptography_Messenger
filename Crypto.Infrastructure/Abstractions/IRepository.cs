namespace Crypto.Infrastructure.Abstractions;

public interface IRepository<T> 
    where T : class
{
    List<T> GetDataList();
    T GetData(int id);
    IRepository<T> AddData(T data);
    IRepository<T> AddDataList(IList<T> data);
}