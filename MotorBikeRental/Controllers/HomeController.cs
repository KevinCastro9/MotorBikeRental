using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRental.IRepository;
using MotorBikeRental.Models;
using MotorBikeRental.Repositories;
using MotorBikeRental.Services;

namespace MotorBikeRental.Controllers
{
    [ApiController]
    [Route("api/v1/home")]
    public class HomeController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromForm] string userName, [FromForm] string passWord)
        {
            var user = _userRepository.GetUserAuthentication(userName, passWord);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
    }
}
