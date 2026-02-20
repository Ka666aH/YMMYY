using Domain.Entities;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface ITranslatorService
    {
        Task<List<string>> TranslateIngredientsAsync(List<string> ingredients, string source, CancellationToken ct = default);
        Task<List<Recipe>> TranslateRecipeAsync(List<Recipe> recipes, string target, CancellationToken ct = default);
    }
}