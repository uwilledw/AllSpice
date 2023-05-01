namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth;

    public RecipesController(RecipesService recipesService, Auth0Provider auth)
    {
        _recipesService = recipesService;
        _auth = auth;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetRecipes()
    {
        try
        {
            List<Recipe> recipes = _recipesService.GetRecipes();
            return Ok(recipes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{recipeId}")]
    public ActionResult<Recipe> GetRecipe(int recipeId)
    {
        try
        {
            Recipe recipe = _recipesService.GetRecipe(recipeId);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{recipeId}/ingredients")]
    public ActionResult<List<Ingredient>> GetRecipeIngs(int recipeId)
    {
        try
        {
            List<Ingredient> ings = _recipesService.GetRecipeIngs(recipeId);
            return Ok(ings);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.CreateRecipe(recipeData);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{recipeId}")]
    [Authorize]
    public ActionResult<Recipe> EditRecipe([FromBody] Recipe recipeData, int recipeId)
    {
        try
        {
            recipeData.Id = recipeId;
            Recipe recipe = _recipesService.EditRecipe(recipeData, recipeId);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{recipeId}")]
    public ActionResult<string> Remove(int recipeId)
    {
        try
        {
            string message = _recipesService.Remove(recipeId);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
