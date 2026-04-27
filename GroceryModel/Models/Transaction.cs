namespace GroceryModel
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TimeStamp{ get; set; }
        public int ProductId { get; set; }
        public string ProductName = ""; // in case product name changes
        public decimal Price { get; set; }
        public int BeforeQty { get; set; }
        public int SoldQty  { get; set; }
        public string CashierName { get; set; } = "";

    }
}
