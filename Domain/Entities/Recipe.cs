namespace Domain.Entities
{
    public class Recipe
    {
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public List<string> UsedIngredients { get; set; }
        public List<string> MissingIngredients { get; set; }

        public Recipe(string title, string imageURL, List<string> usedIngredients, List<string> missingIngredients)
        {
            Title = title;
            ImageURL = imageURL;
            UsedIngredients = usedIngredients;
            MissingIngredients = missingIngredients;
        }
    }
}