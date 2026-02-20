namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ICache
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, CancellationToken ct = default); 
    }
}