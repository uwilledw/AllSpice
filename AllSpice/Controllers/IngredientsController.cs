namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth;

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth)
    {
        _ingredientsService = ingredientsService;
        _auth = auth;
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Ingredient> AddIng([FromBody] Ingredient ingData)
    {
        try
        {
            Ingredient ing = _ingredientsService.AddIng(ingData);
            return Ok(ing);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{ingId}")]
    [Authorize]

    public ActionResult<string> Remove(int ingId)
    {
        try
        {
            string message = _ingredientsService.Remove(ingId);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
