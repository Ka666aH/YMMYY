namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ICache
    {
        Task<string> GetOrCreateAsync(string key, Func<Task<string>> factory, CancellationToken ct = default); 
    }
}
