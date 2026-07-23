namespace TheShop.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }

        public List<BasketItemDto> BasketItems { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return BasketItems?.Sum(x => x.UnitPrice * x.Quantity) ?? 0;
            }
        }
    }
}
