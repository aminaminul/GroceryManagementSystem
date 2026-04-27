using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryModel
{
    public class Batch
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string BatchNumber { get; set; } = string.Empty;
        [Required]
        public int InitialQuantity { get; set; }
        [Required]
        public int RemainingQuantity { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public decimal? CostPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
