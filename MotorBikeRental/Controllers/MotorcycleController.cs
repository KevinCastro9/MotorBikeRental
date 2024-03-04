using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRental.Enum;
using MotorBikeRental.IRepository;
using MotorBikeRental.Models;
using MotorBikeRental.ViewModels;

namespace MotorBikeRental.Controllers
{
    [ApiController]
    [Route("api/v1/motorcycle")]
    public class MotorcycleController : ControllerBase
    {

        private readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleController(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(MotorcycleView motorcycleView)
        {
            try
            {
                var motorcycle = new Motorcycle(motorcycleView.Ano, motorcycleView.Modelo, motorcycleView.Placa, motorcycleView.StatusLocacao);
                var result = _motorcycleRepository.Add(motorcycle);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(motorcycle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(MotorcycleView motorcycleView)
        {
            try
            {
                var motorcycle = new Motorcycle(motorcycleView.Ano, motorcycleView.Modelo, motorcycleView.Placa, motorcycleView.StatusLocacao);
                motorcycle.ID = motorcycleView.ID;

                var result = _motorcycleRepository.Update(motorcycle);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(motorcycle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _motorcycleRepository.Remove(id);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(String.Format("Moto Id: {0} deletada com sucesso", id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetList")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var motorcycle = _motorcycleRepository.GetList();

                return Ok(motorcycle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            try
            {
                var motorcycle = _motorcycleRepository.GetById(id);
                return Ok(motorcycle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetByPlate/{plate}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByPlate(string plate)
        {
            try
            {
                var motorcycle = _motorcycleRepository.GetByPlate(plate);
                return Ok(motorcycle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
