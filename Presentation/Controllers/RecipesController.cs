using Application.Interfaces.ServiceInterfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesFinder _rf;

        public RecipesController(IRecipesFinder rf)
        {
            _rf = rf;
        }

        [HttpPost("by_ingredients")]
        public async Task<IActionResult> FindRecipesByIngredients([FromBody] List<string> ingredients, CancellationToken ct)
        {
            string sourceLanguage = Languages.Russian;
            var recipes =  await _rf.FindRecipesAsync(ingredients, sourceLanguage, ct);
            return Ok(recipes);
        }
    }
}
