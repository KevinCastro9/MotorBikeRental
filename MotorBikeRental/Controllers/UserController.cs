using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRental.IRepository;
using MotorBikeRental.Models;
using MotorBikeRental.Repositories;
using MotorBikeRental.ViewModels;

namespace MotorBikeRental.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(UserView userView)
        {
            var user = new User(userView.UserName, userView.PassWord, userView.Role, userView.Status) ;
            _userRepository.Add(user);
            return Ok(user);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(UserView userView)
        {
            var user = new User(userView.UserName, userView.PassWord, userView.Role, userView.Status);
            user.ID = userView.ID;

            _userRepository.Update(user);
            user.Password = "";
            return Ok(user);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromForm] int id)
        {
            
            _userRepository.Remove(id);
            return Ok(String.Format("Usuario Id: {0} deletado com sucesso", id));
        }

        [HttpGet]
        [Route("GetList")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            var user = _userRepository.GetList();

            return Ok(user);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById([FromForm] int id)
        {
            var user = _userRepository.GetById(id);
            return Ok(user);
        }
    }
}
