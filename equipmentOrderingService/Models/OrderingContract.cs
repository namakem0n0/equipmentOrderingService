using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace equipmentOrderingService.Models
{
    public class OrderingContract
    {
        [Key]
        public Guid Id { get; set; }
        public int EquipmentQuantity { get; set; }
        public IndustrialPremises? Premises { get; set; }
        public TechnicalEquipment? EquipmentType { get; set; }   
    }
}
