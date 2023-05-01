namespace AllSpice.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth;

    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth)
    {
        _favoritesService = favoritesService;
        _auth = auth;
    }

    [HttpPost]
    public async Task<ActionResult<Favorite>> CreateFav([FromBody] Favorite favData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            favData.AccountId = userInfo.Id;
            Favorite favorite = _favoritesService.CreateFav(favData);
            return Ok(favorite);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{favId}")]
    public async Task<ActionResult<string>> RemoveFav(int favId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            string message = _favoritesService.RemoveFav(favId);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}


