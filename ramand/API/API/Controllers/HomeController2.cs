
using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.5")]
    public class HomeController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public HomeController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
      
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
           var res=await _authenticateService.Login(userLogin);
            return Ok(res);
        }
  
        
    }
}
