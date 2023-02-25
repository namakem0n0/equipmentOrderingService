using equipmentOrderingService.Models;

namespace equipmentOrderingService.IRepositories
{
    public interface ITechnicalEquipmentRepository : IGenericRepository<TechnicalEquipment>
    {
        Task<TechnicalEquipment> GetByName(string name);
    }
}
