using equipmentOrderingService.IConfiguration;
using equipmentOrderingService.IRepositories;
using equipmentOrderingService.Repositories;

namespace equipmentOrderingService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IIndustrialPremisesRepository industrialPremises { get; private set; }  

        public ITechnicalEquipmentRepository technicalEquipment { get; private set; }

        public IOrderingContractRepository orderingContract { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger= loggerFactory.CreateLogger("logs");

            industrialPremises = new IndustrialPremisesRepository(_context, _logger);
            technicalEquipment = new TechnicalEquipmentRepository(_context, _logger);
            orderingContract = new OrderingContractRepository(_context, _logger);

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        /*public async Task Dispose()
        {
            await _context.DisposeAsync();
        }*/

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
