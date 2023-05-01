namespace AllSpice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient AddIng(Ingredient ingData)
    {
        string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId)
            VALUES
            (@name, @quantity, @recipeId);

            SELECT 
            ing.*
            FROM ingredients ing
            WHERE ing.id = LAST_INSERT_ID();
            ;";

        Ingredient ing = _db.Query<Ingredient>(sql, ingData).FirstOrDefault();
        return ing;
    }

    internal List<Ingredient> GetRecipeIngs(int recipeId)
    {
        string sql = @"
            SELECT
            *
            FROM ingredients
            WHERE ingredients.recipeId = @recipeId
            ;";

        List<Ingredient> ings = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return ings;
    }

    internal void Remove(int ingId)
    {
        string sql = @"
        DELETE FROM ingredients WHERE id = @ingId LIMIT 1
        ;";
        _db.Execute(sql, new { ingId });
    }
}
