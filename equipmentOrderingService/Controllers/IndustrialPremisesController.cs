using equipmentOrderingService.IConfiguration;
using equipmentOrderingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace equipmentOrderingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndustrialPremisesController : ControllerBase
    {
        private readonly ILogger<IndustrialPremisesController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public IndustrialPremisesController(ILogger<IndustrialPremisesController> logger, IUnitOfWork unitOfWork)
        {
            _logger= logger;
            _unitOfWork= unitOfWork;
        }

        //Add new premises
        [HttpPost]
        public async Task<IActionResult> CreatePremises(IndustrialPremises industrialPremises)
        {
            if(ModelState.IsValid)
            {
                industrialPremises.Id = Guid.NewGuid();

                await _unitOfWork.industrialPremises.Add(industrialPremises);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetPremises", new { industrialPremises.Id }, industrialPremises);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        //Get premises by id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetPremises(Guid id)
        {
            var premises = await _unitOfWork.industrialPremises.GetById(id);

            if(premises == null)
                return NotFound(); // 404

            return Ok(premises);
        }

        //Get all premises
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var industrialPremises = await _unitOfWork.industrialPremises.GetAll();
            return Ok(industrialPremises);
        }

        //Update premises by id
        [HttpPut]
        [Route("{id:guid}")]
        [ActionName("UpdatePremisesById")]
        public async Task<IActionResult> UpdatePremises(Guid id, IndustrialPremises industrialPremises)
        {
            if(id!=industrialPremises.Id) 
                return BadRequest();
            await _unitOfWork.industrialPremises.Update(industrialPremises);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        //Delete premises
        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeletePremisesById")]
        public async Task<IActionResult> DeletePremises(Guid id)
        {
            var item = await _unitOfWork.industrialPremises.GetById(id);

            if (item == null)
                return BadRequest();
            await _unitOfWork.industrialPremises.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }

    }
}
