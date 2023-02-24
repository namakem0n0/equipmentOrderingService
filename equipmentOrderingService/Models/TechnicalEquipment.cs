using System.ComponentModel.DataAnnotations;

namespace equipmentOrderingService.Models
{
    public class TechnicalEquipment
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Area { get; set; }
    }
}
