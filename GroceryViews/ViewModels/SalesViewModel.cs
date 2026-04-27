using System.ComponentModel.DataAnnotations;
using GroceryModel;
using GroceryViews.ViewModels.Validations;

namespace GroceryViews.ViewModels
{
    public class SalesViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<Category> Categories{ get; set; } = new List<Category>();
        public int SelectedProductId { get; set; }
        
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue)]
        [SalesViewModel_EnsureProperQuantity]
        public int QuantityToSell { get; set; }

        // Payment fields
        [Display(Name = "Payment Method")]
        public PaymentMethod SelectedPaymentMethod { get; set; }

        [Display(Name = "Transaction ID (Mobile Payment)")]
        public string? MobileTransactionId { get; set; }

        [Display(Name = "Card Number")]
        public string? CardNumber { get; set; }
    }
}
