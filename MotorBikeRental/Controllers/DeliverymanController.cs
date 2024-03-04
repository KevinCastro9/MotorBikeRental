using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRental.IRepositories;
using MotorBikeRental.Models;
using MotorBikeRental.Repositories;
using MotorBikeRental.ViewModels;

namespace MotorBikeRental.Controllers
{
    [ApiController]
    [Route("api/v1/deliveryman")]
    public class DeliverymanController : Controller
    {
        private readonly IDeliverymanRepository _deliverymanRepository;

        public DeliverymanController(IDeliverymanRepository deliverymanRepository)
        {
            _deliverymanRepository = deliverymanRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [Route("Add")]
        [AllowAnonymous]
        public IActionResult Add([FromForm] DeliverymanView deliverymanView)
        {
            try
            {
                if (deliverymanView.ImageCnh == null)
                    return BadRequest(String.Format("É obrigatorio adicionar a imagem da CNH no momento do cadastro"));

                var deliveryman = new Deliveryman(deliverymanView.Username, deliverymanView.Password, deliverymanView.Cnpj, deliverymanView.Dateofbirth, deliverymanView.Codcnh, deliverymanView.Typecnh, Path.Combine("Storage", deliverymanView.Codcnh + "- CNH"), deliverymanView.Status);
                var result = _deliverymanRepository.Add(deliveryman, deliverymanView.ImageCnh);

                if (result == false)
                    return BadRequest();

                return Ok(deliveryman);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromForm] DeliverymanView deliverymanView)
        {
            try
            {
                var path = "";
                if(deliverymanView.ImageCnh != null)
                {
                    path = Path.Combine("Storage", deliverymanView.Codcnh + "- CNH");
                }

                var deliveryman = new Deliveryman(deliverymanView.Username, deliverymanView.Password, deliverymanView.Cnpj, deliverymanView.Dateofbirth, deliverymanView.Codcnh, deliverymanView.Typecnh, path, deliverymanView.Status);
                deliveryman.Id = deliverymanView.Id;

                var result = _deliverymanRepository.Update(deliveryman, deliverymanView.ImageCnh);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(deliveryman);
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
                var result = _deliverymanRepository.Remove(id);

                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(String.Format("Entregador Id: {0} deletado com sucesso", id));
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
                var deliverymans = _deliverymanRepository.GetList();

                return Ok(deliverymans);
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
                var deliveryman = _deliverymanRepository.GetById(id);
                return Ok(deliveryman);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
