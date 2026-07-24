using TheShop.Order.Dtos.OrderDetailDtos;

namespace TheShop.Order.Dtos.CustomerOrderDtos
{
    public class ResultCustomerOrderDto
    {
        public int CustomerOrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }

        public List<ResultOrderDetailDto> OrderDetails { get; set; }
    }
}
