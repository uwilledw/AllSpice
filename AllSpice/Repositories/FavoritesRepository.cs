namespace AllSpice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite CreateFav(Favorite favData)
    {
        string sql = @"
            INSERT INTO
            favorites(recipeId, accountId)
            VALUES(@recipeId, @accountId);
            SELECT LAST_INSERT_ID();
            ";

        int id = _db.ExecuteScalar<int>(sql, favData);
        favData.Id = id;
        return favData;
    }

    internal List<MyFavorite> GetMyFavorites(string userId)
    {
        string sql = @"
            SELECT
            fav.*,
            rec.*,
            prof.*
            FROM favorites fav
            JOIN recipes rec ON fav.recipeId = rec.id
            JOIN accounts prof ON rec.creatorId = prof.id
            WHERE fav.accountId = @userId;
            ";

        List<MyFavorite> favorites = _db.Query<Favorite, MyFavorite, Profile, MyFavorite>
        (sql, (fav, rec, prof) =>
        {
            rec.Creator = prof;
            rec.FavoriteId = fav.Id;
            return rec;
        }, new { userId }).ToList();
        return favorites;
    }

    internal void RemoveFav(int favId)
    {
        string sql = @"
        DELETE FROM favorites WHERE id = @favId
        ;";
        _db.Execute(sql, new { favId });
    }
}
