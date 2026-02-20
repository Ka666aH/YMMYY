using Domain.Entities;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IRecipesSourceService
    {
        Task<List<Recipe>> GetRecipesAsync(List<string> ingredients_en, CancellationToken ct);
    }
}