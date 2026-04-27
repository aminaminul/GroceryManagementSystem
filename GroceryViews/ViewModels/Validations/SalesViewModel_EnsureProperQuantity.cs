using System.ComponentModel.DataAnnotations;
using GroceryModel;

namespace GroceryViews.ViewModels.Validations
{
    public class SalesViewModel_EnsureProperQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var salesViewModel = validationContext.ObjectInstance as SalesViewModel;
            
            var productsService = validationContext.GetService(typeof(GroceryService.Interfaces.IProductsService)) as GroceryService.Interfaces.IProductsService;

            if (salesViewModel != null && productsService != null) 
            {
                if (salesViewModel.QuantityToSell <= 0)
                {
                    return new ValidationResult("The quantity to sell has to be greater than zero");
                }
                else
                {
                    var product = productsService.GetProductById(salesViewModel.SelectedProductId);
                    if (product != null)
                    {
                        if (product.StockQuantity < salesViewModel.QuantityToSell)
                        {
                            return new ValidationResult($"[{product.Name}] only has {product.StockQuantity} left.");
                        }
                    }
                    else
                    {
                        return new ValidationResult("The selected product doesn't exist.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
