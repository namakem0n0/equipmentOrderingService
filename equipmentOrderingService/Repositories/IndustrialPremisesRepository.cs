using equipmentOrderingService.Data;
using equipmentOrderingService.IRepositories;
using equipmentOrderingService.Models;
using Microsoft.EntityFrameworkCore;

namespace equipmentOrderingService.Repositories
{
    public class IndustrialPremisesRepository : GenericRepository<IndustrialPremises>, IIndustrialPremisesRepository
    {
        public IndustrialPremisesRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
               
        }

        public override async Task<IEnumerable<IndustrialPremises>> GetAll()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "{Repo} GetAll method error", typeof(IndustrialPremisesRepository));
                return new List<IndustrialPremises>();
            }
        }
        
        public override async Task<bool> Update(IndustrialPremises entity)
        {

            try
            {
                var existingPremises = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existingPremises == null)
                    return await Add(entity);
                existingPremises.Name = entity.Name;
                existingPremises.Area = entity.Area;

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo}  Update method error", typeof(IndustrialPremisesRepository));
                return false;

            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x=> x.Id == id).FirstOrDefaultAsync();

                if (exist != null)
                {
                    dbSet.Remove(exist);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo}  Delete method error", typeof(IndustrialPremisesRepository));
                return false;
            }
        }
    }
}
