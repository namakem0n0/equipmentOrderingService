using equipmentOrderingService.Data;
using equipmentOrderingService.IConfiguration;
using equipmentOrderingService.IRepositories;
using equipmentOrderingService.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace equipmentOrderingService.Repositories
{
    public class OrderingContractRepository : GenericRepository<OrderingContract>, IOrderingContractRepository
    {
        public OrderingContractRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<OrderingContract>> GetAll()
        {
            try
            {
                return await dbSet.Include(oc=>oc.Premises).Include(oc=>oc.EquipmentType).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll method error", typeof(OrderingContractRepository));
                return new List<OrderingContract>();
            }
        }

        public override async Task<bool> Update(OrderingContract entity)
        {

            try
            {
                var existingСontract = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existingСontract == null)
                    return await Add(entity);
                existingСontract.Premises = entity.Premises;
                existingСontract.EquipmentType = entity.EquipmentType;
                existingСontract.EquipmentQuantity = entity.EquipmentQuantity;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo}  Update method error", typeof(OrderingContractRepository));
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
                _logger.LogError(ex, "{Repo}  Delete method error", typeof(OrderingContractRepository));
                return false;
            }
        }
    }
}
