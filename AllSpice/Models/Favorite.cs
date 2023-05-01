namespace AllSpice.Models;

public class Favorite
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string AccountId { get; set; }
}


public class MyFavorite : Recipe
{
    public int FavoriteId { get; set; }
}