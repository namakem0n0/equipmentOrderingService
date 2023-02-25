using equipmentOrderingService.Models;

namespace equipmentOrderingService.IRepositories
{
    public interface IOrderingContractRepository : IGenericRepository<OrderingContract>
    {
        //Task<OrderingContract> GetContractWithDetailsAsync(Guid ContractId);
    }
}
