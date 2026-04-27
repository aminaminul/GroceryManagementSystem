using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryModel
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; } = new List<PurchaseDetails>();
    }
}
