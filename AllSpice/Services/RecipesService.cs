namespace AllSpice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repo.CreateRecipe(recipeData);
        return recipe;
    }

    internal Recipe EditRecipe(Recipe recipeData, int recipeId)
    {
        Recipe originalRecipe = this.GetRecipe(recipeId);

        originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;
        originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;
        originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;
        originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;

        _repo.EditRecipe(originalRecipe);

        return originalRecipe;
    }

    internal Recipe GetRecipe(int recipeId)
    {
        Recipe recipe = _repo.GetRecipe(recipeId);
        if (recipe == null)
        {
            throw new Exception($"Id:{recipeId}, is bad");
        }
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        List<Recipe> recipes = _repo.GetRecipes();
        return recipes;
    }

    internal string Remove(int recipeId)
    {
        Recipe recipe = this.GetRecipe(recipeId);
        int rowsAffected = _repo.Remove(recipeId);

        if (rowsAffected == 0)
        {
            throw new Exception("Delete failed");
        }
        if (rowsAffected > 1)
        {
            throw new Exception("Get help");
        }
        return $"{recipe.Title} was terrible.";
    }
}
