using equipmentOrderingService.Models;

namespace equipmentOrderingService.IRepositories
{
    public interface IIndustrialPremisesRepository : IGenericRepository<IndustrialPremises>
    {
        Task<IndustrialPremises> GetByName(string name);
    }
}
