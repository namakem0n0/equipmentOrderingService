using equipmentOrderingService.IRepositories;

namespace equipmentOrderingService.IConfiguration
{
    public interface IUnitOfWork
    {
        IIndustrialPremisesRepository industrialPremises { get; }
        ITechnicalEquipmentRepository technicalEquipment { get; }
        IOrderingContractRepository orderingContract { get; }

        Task CompleteAsync();
    }
}
