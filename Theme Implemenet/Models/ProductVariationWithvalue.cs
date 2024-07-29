namespace Theme_Implemenet.Models
{
    public class ProductVariationWithvalue
    {
        public int productId { get; set; }
        public virtual Product Product { get; set; }    
        public string image { get; set; }
        public int Price { get; set; }
        public double? Discount { get; set; }
        public decimal DiscountedPrice
        {
            get
            {
                return Discount.HasValue ? Price - (Price * (decimal)Discount.Value / 100) : Price;
            }
        }
    }
}
