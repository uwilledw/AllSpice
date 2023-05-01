namespace AllSpice.Services
{
    public class IngredientsService
    {
        private readonly IngredientsRepository _repo;

        public IngredientsService(IngredientsRepository repo)
        {
            _repo = repo;
        }

        internal Ingredient AddIng(Ingredient ingData)
        {
            Ingredient ing = _repo.AddIng(ingData);
            return ing;
        }

        internal List<Ingredient> GetRecipeIngs(int recipeId)
        {
            List<Ingredient> ings = _repo.GetRecipeIngs(recipeId);
            return ings;
        }

        internal string Remove(int ingId)
        {
            _repo.Remove(ingId);
            return "Ingredient was removed";
        }
    }
}