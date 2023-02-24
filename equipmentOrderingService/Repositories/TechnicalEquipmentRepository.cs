using equipmentOrderingService.Data;
using equipmentOrderingService.IRepositories;
using equipmentOrderingService.Models;
using Microsoft.EntityFrameworkCore;

namespace equipmentOrderingService.Repositories
{
    public class TechnicalEquipmentRepository : GenericRepository<TechnicalEquipment>, ITechnicalEquipmentRepository
    {
        public TechnicalEquipmentRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<IEnumerable<TechnicalEquipment>> GetAll()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll method error", typeof(TechnicalEquipmentRepository));
                return new List<TechnicalEquipment>();
            }
        }
        public override async Task<bool> Update(TechnicalEquipment entity)
        {

            try
            {
                var existingEquipment = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existingEquipment == null)
                    return await Add(entity);
                existingEquipment.Name = entity.Name;
                existingEquipment.Area = entity.Area;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo}  Upsert method error", typeof(TechnicalEquipmentRepository));
                return false;

            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (exist != null)
                {
                    dbSet.Remove(exist);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo}  Upsert method error", typeof(TechnicalEquipmentRepository));
                return false;
            }
        }
    }
}