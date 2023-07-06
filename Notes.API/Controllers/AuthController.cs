using Microsoft.AspNetCore.Mvc;

using Notes.API.Data;
using Notes.API.Repositories;

namespace Notes.API.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IUserRepository userRepository;
    private readonly ITokenHandler tokenHandler;
    private readonly TableDbContext _tableDbContext;

    public AuthController(TableDbContext tableDbContext, IUserRepository userRepository, ITokenHandler tokenHandler)
    {
        _tableDbContext = tableDbContext;
        this.userRepository = userRepository;
        this.tokenHandler = tokenHandler;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
    {
        //check auth
        var user = await userRepository.AuthenticateAsync(
            loginRequest.UserName, loginRequest.Password);

        if (user != null)
        {
            var token = await tokenHandler.CreateTokenAsync(user);
            return Ok(token);
        }
        return BadRequest("Błedne dane");
    }
}

