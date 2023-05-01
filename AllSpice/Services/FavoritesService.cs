namespace AllSpice.Services
{
    public class FavoritesService
    {
        private readonly FavoritesRepository _repo;

        public FavoritesService(FavoritesRepository repo)
        {
            _repo = repo;
        }

        internal Favorite CreateFav(Favorite favData)
        {
            Favorite favorite = _repo.CreateFav(favData);
            return favorite;
        }

        internal List<MyFavorite> GetMyFavorites(string userId)
        {
            List<MyFavorite> favorites = _repo.GetMyFavorites(userId);
            return favorites;
        }

        internal string RemoveFav(int favId)
        {
            _repo.RemoveFav(favId);
            return "Recipe was removed.";
        }
    }
}