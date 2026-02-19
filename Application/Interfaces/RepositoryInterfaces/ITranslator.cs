namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ITranslator
    {
        //Task<string> TranslateIngredientToEnAsync(string ingredient_ru, CancellationToken ct = default);
        ////Task<Recipe> TranslateRecipeAsync(Recipe recipe_en, CancellationToken ct = default);
        //Task<string> TranslateRecipeTitleToRuAsync(string recipeTitle_en, CancellationToken ct = default);
        //Task<string> TranslateIngredientOriginToRuAsync(string ingredientOrigin_en, CancellationToken ct = default);
        Task<string> TranslateAsync(string word, string source, string target, CancellationToken ct = default);
    }
}
