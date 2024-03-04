using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRental.Enum;
using MotorBikeRental.IRepositories;
using MotorBikeRental.IRepository;
using MotorBikeRental.Models;
using MotorBikeRental.Repositories;
using MotorBikeRental.ViewModels;

namespace MotorBikeRental.Controllers
{
    [ApiController]
    [Route("api/v1/location")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IDeliverymanRepository _deliverymanRepositoryRepository;

        public LocationController(ILocationRepository locationRepository, IMotorcycleRepository motorcycleRepository, IDeliverymanRepository deliverymanRepositoryRepository)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException();
            _motorcycleRepository = motorcycleRepository;
            _deliverymanRepositoryRepository = deliverymanRepositoryRepository;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(LocationView locationView)
        {
            try
            {
                var location = new Location(locationView.Startdate, locationView.Enddate, locationView.Valueforecast, locationView.Status, locationView.Idmotorcycle, locationView.Iddeliveryman);
                var result = _locationRepository.Add(location);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(location);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(LocationView locationView)
        {
            try
            {
                var location = new Location(locationView.Startdate, locationView.Enddate, locationView.Valueforecast, locationView.Status, locationView.Idmotorcycle, locationView.Iddeliveryman);
                location.Id = locationView.Id;

                var result = _locationRepository.Update(location);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(location);
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
                var result = _locationRepository.Remove(id);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(String.Format("Locação Id: {0} deletada com sucesso", id));
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
                var locations = _locationRepository.GetList();

                return Ok(locations);
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
                var location = _locationRepository.GetById(id);
                return Ok(location);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("RentMotorbike")]
        [AllowAnonymous]
        public IActionResult RentMotorbike([FromForm] string userName, [FromForm] string password, [FromForm] DateTime enddate, [FromForm] int quantidadeDias)
        {
            try
            {
                var deliveryman = _deliverymanRepositoryRepository.DeliveryManValidation(userName, password);

                if(deliveryman != null)
                {
                    var motorcycle = _motorcycleRepository.GetMotorcycleAvailable();

                    if (motorcycle != null)
                    {
                        var result = _locationRepository.RentMotorbike(DateTime.Now.AddDays(1), enddate, quantidadeDias, (int)motorcycle.ID, deliveryman.Id);

                        if (result == 0)
                        {
                            return BadRequest(String.Format("Não foi possivel realizar a locação"));
                        }

                        motorcycle.Statulocacao = 1;
                        _motorcycleRepository.Update(motorcycle);
                        RentMotorbikeView rentMotorbikeView = new RentMotorbikeView(DateTime.Now.AddDays(1), enddate, motorcycle.Modelo, motorcycle.Placa, result);
                        return Ok(rentMotorbikeView);
                    }
                    else
                    {
                        return BadRequest(String.Format("Não existem motos disponiveis para locação"));
                    }
                }
                else
                    return BadRequest(String.Format("Entregador não identificado, por gentileza realize o cadastro!"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("DevolutionMotorbike")]
        [AllowAnonymous]
        public IActionResult DevolutionMotorbike([FromForm] string userName, [FromForm] string password)
        {
            try
            {
                var deliveryman = _deliverymanRepositoryRepository.DeliveryManValidation(userName, password);

                if (deliveryman != null)
                {
                    var returnLocation = _locationRepository.DevolutionMotorbike(deliveryman.Id);

                    if (returnLocation != null)
                    {
                        var motocycle = _motorcycleRepository.GetById(returnLocation.IdMotorcycle);
                        motocycle.Statulocacao = 0;
                        _motorcycleRepository.Update(motocycle);

                        return Ok(String.Format("Preço final da locação: {0}. Moto modelo: {1}", returnLocation.PrinceFinal, motocycle.Modelo));
                    }
                    else
                        return BadRequest(String.Format("Não foi possivel finalizar a locação por gentileza tente novamente"));
                }
                else
                    return BadRequest(String.Format("Entregador não identificado, por gentileza realize o cadastro!"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

    }
}
