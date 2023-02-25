using equipmentOrderingService.IConfiguration;
using equipmentOrderingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace equipmentOrderingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TechnicalEquipmentController : ControllerBase
    {
        private readonly ILogger<TechnicalEquipmentController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TechnicalEquipmentController(ILogger<TechnicalEquipmentController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //Get all equipment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var technicalEquipment = await _unitOfWork.technicalEquipment.GetAll();
            return Ok(technicalEquipment);
        }

        //Get equipment by id
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetEquipmentById")]
        public async Task<IActionResult> GetEquipment(Guid id)
        {
            var equipment = await _unitOfWork.technicalEquipment.GetById(id);

            if (equipment == null)
                return NotFound(); // 404

            return Ok(equipment);
        }

        //Add new equipment
        [HttpPost]
        public async Task<IActionResult> CreateEquipment(TechnicalEquipment technicalEquipment)
        {
            if (ModelState.IsValid)
            {
                technicalEquipment.Id = Guid.NewGuid();

                await _unitOfWork.technicalEquipment.Add(technicalEquipment);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetEquipment", new { technicalEquipment.Id }, technicalEquipment);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        //Update equipment
        [HttpPut]
        [Route("{id:guid}")]
        [ActionName("UpdateEquipmentById")]
        public async Task<IActionResult> UpdateEquipment(Guid id, TechnicalEquipment technicalEquipment)
        {
            if (id != technicalEquipment.Id)
                return BadRequest();
            await _unitOfWork.technicalEquipment.Update(technicalEquipment);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        //Delete equipment
        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteEquipmentById")]
        public async Task<IActionResult> DeleteEquipment(Guid id)
        {
            var item = await _unitOfWork.technicalEquipment.GetById(id);

            if (item == null)
                return BadRequest();
            await _unitOfWork.technicalEquipment.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }


    }
}
