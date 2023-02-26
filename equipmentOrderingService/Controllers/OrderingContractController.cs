using equipmentOrderingService.IConfiguration;
using equipmentOrderingService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace equipmentOrderingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderingContractController : ControllerBase
    {
        private readonly ILogger<OrderingContractController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public OrderingContractController(ILogger<OrderingContractController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //Get all contracts
        [HttpGet]
        [Route("GetAllContracts")]
        public async Task<IActionResult> GetAll()
        {
            var orderingContract = await _unitOfWork.orderingContract.GetAll();
            return Ok(orderingContract);
        }

        //Get contract by id
        [HttpGet]
        [Route("GetContractByNames")]
        [ActionName("GetContractByNames")]
        public async Task<IActionResult> GetContract(string premisesName, string equipmentName, int Quantity)
        {
            var orderingContract = await _unitOfWork.orderingContract
                .GetWhere(oc => oc.Premises.Name == premisesName 
                && oc.EquipmentType.Name == equipmentName 
                && oc.EquipmentQuantity == Quantity);

            if (orderingContract == null)
                return NotFound(); // 404

            return Ok(orderingContract);
        }

        //Add new contract
        [HttpPost]
        public async Task<IActionResult> CreateContract(Guid industrialPremisesId, Guid technicalEquipmentId, int Quantity)
        {
            if (ModelState.IsValid)
            {
                var existingPremises = await _unitOfWork.industrialPremises.GetById(industrialPremisesId);
                var existingEquipment = await _unitOfWork.technicalEquipment.GetById(technicalEquipmentId);

                if (existingPremises == null || existingEquipment == null || Quantity <= 0) 
                    return BadRequest();

                if (existingEquipment.Area * Quantity > existingPremises.FreeArea)
                    return new JsonResult("Not enoughf available space") { StatusCode = 500 };

                var orderingContract = new OrderingContract();
                orderingContract.Id = Guid.NewGuid();
                orderingContract.Premises = existingPremises;
                orderingContract.EquipmentType = existingEquipment;
                orderingContract.EquipmentQuantity = Quantity;

                var usedArea = existingEquipment.Area * Quantity;
                existingPremises.FreeArea -= usedArea;

                await _unitOfWork.industrialPremises.Update(existingPremises);
                await _unitOfWork.orderingContract.Add(orderingContract);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetContract", new { orderingContract.Id }, orderingContract);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        //Update contract
        [HttpPut]
        [Route("{id:guid}")]
        [ActionName("UpdateContractById")]
        public async Task<IActionResult> UpdateContract(Guid id, OrderingContract orderingContract)
        {
            if (id != orderingContract.Id)
                return BadRequest();
            await _unitOfWork.orderingContract.Update(orderingContract);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        //Delete contract
        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteContractById")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            var item = await _unitOfWork.orderingContract.GetById(id);

            if (item == null)
                return BadRequest();
            await _unitOfWork.orderingContract.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}