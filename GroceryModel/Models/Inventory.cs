using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryModel
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int TotalQuantity { get; set; }
        [Required]
        public int LowStockThreshold { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        
        [NotMapped]
        public bool IsLowStock => TotalQuantity <= LowStockThreshold;
    }
}
