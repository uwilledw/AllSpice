namespace AllSpice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    public List<Recipe> GetRecipes()
    {
        string sql = @"
        SELECT 
        rec.*,
        creator.*
        FROM recipes rec 
        JOIN accounts creator ON creator.id = rec.creatorId
        ;";

        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, creator) =>
        {
            recipe.Creator = creator;
            return recipe;
        }).ToList();
        return recipes;
    }

    public Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes
        (title, instructions, category, img, creatorId)
        VALUES 
        (@title, @instructions, @category, @img, @creatorId);
        
        SELECT 
        rec.*,
        creator.*
        FROM recipes rec
        JOIN accounts creator ON rec.CreatorId = creator.id
        WHERE rec.id = LAST_INSERT_ID();
        ";
        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, creator) =>
        {
            recipe.Creator = creator;
            return recipe;
        }, recipeData).FirstOrDefault();
        return recipe;
    }

    internal Recipe GetRecipe(int recipeId)
    {
        string sql = @"
        SELECT
        rec.*,
        creator.*
        FROM recipes rec
        JOIN accounts creator ON rec.creatorId = creator.id
        WHERE rec.id = @recipeId
        ;";
        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, creator) =>
        {
            recipe.Creator = creator;
            return recipe;
        }, new { recipeId }).FirstOrDefault();
        return recipe;
    }

    internal void EditRecipe(Recipe originalRecipe)
    {
        string sql = @"
        UPDATE recipes
        SET
        title = @Title,
        instructions = @Instructions,
        category = @Category,
        img = @Img
        WHERE id = @Id
        ;";
        _db.Execute(sql, originalRecipe);
    }

    internal int Remove(int recipeId)
    {
        string sql = @"
        DELETE FROM recipes
        WHERE id = @recipeId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { recipeId });
        return rowsAffected;
    }
}
