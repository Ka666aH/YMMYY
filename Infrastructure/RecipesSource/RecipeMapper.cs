using Domain.Entities;

namespace Infrastructure.RecipesSource
{
    public static class RecipeMapper
    {
        public static List<Recipe> ToRecipes(this List<SpoonacularRecipe> spoonacularRecipes) => spoonacularRecipes.Select(sp =>
            new Recipe(
                sp.Title,
                sp.Image,
                sp.UsedIngredients.Select(i => i.Original).ToList(),
                sp.MissedIngredients.Select(i => i.Original).ToList()
                )
        ).ToList();
    }
}
