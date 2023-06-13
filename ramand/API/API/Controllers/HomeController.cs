
using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("2.0")]
    public class HomeController2 : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public HomeController2(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var res = await _authenticateService.Login(userLogin);
            return Ok(res);
        }

    }
}
