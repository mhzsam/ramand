using Application.DTO;
using Application.Services.Interface;
using Domain.Entities;
using Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.5")]
    [Authorize]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///  از ساختن سرویس برای یوزر صرف نظر شده و خود ریپازیتوری اینجکت کردیم
        ///  وفت کم بود 
        /// </summary>
        private readonly IUserRepository _userRepository;
        private readonly IRabbitService _rabbitService;
        public UserController(IUserRepository userRepository, IRabbitService rabbitService )
        {
            _userRepository = userRepository;
            _rabbitService = rabbitService;
        }

        [HttpGet]
        public async Task<List<User> > GetAllUser()
        {
            var res= await _userRepository.GetAll();
            return res;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SendUser1ToRabbitMq()
        {
            var res = await _userRepository.GetAll();
            var user= res.FirstOrDefault(s=>s.Id==1);
            string stringObj = JsonSerializer.Serialize(user);
            _rabbitService.SendMessage("test", stringObj);
            return Ok(res);
        }
    }
}
