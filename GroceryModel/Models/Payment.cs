using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryModel
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int OrderId { get; set; }
        
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        
        [Required]
        public decimal PaidAmount { get; set; }
        
        public string? MobileTransactionId { get; set; } // For Bkash/Nagad
        
        public string? CardNumber { get; set; } // Masked or partial for Card
        
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}
