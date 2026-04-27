using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryModel
{
    public class InventoryLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public InventoryChangeType ChangeType { get; set; }
        [Required]
        public int QuantityChanged { get; set; }
        [Required]
        public int PreviousStock { get; set; }
        [Required]
        public int NewStock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
