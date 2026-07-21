namespace TheShop.Discount.Entities
{
    public class DiscountCode
    {
        public string DiscountCodeId { get; set; }
        public string Code { get; set; }
        public int Rate  { get; set; }
        public bool IsActive { get; set; }
    }
}
