using Microsoft.EntityFrameworkCore;
using TheShop.Order.Context;
using TheShop.Order.Dtos.CustomerOrderDtos;
using TheShop.Order.Dtos.OrderDetailDtos;
using TheShop.Order.Entities;

namespace TheShop.Order.Services.CustomerOrderServices
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly OrderContext _context;

        public CustomerOrderService(OrderContext context)
        {
            _context = context;
        }

        public async Task<List<ResultCustomerOrderDto>> GetAllCustomerOrdersAsync()
        {
            var orders = await _context.CustomerOrders
                .Include(x => x.OrderDetails)
                .AsNoTracking()
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();

            return orders.Select(x => new ResultCustomerOrderDto
            {
                CustomerOrderId = x.CustomerOrderId,
                UserId = x.UserId,
                OrderDate = x.OrderDate,
                TotalPrice = x.TotalPrice,
                OrderStatus = x.OrderStatus,
                ReceiverName = x.ReceiverName,
                ReceiverSurname = x.ReceiverSurname,
                PhoneNumber = x.PhoneNumber,
                AddressLine = x.AddressLine,
                City = x.City,
                District = x.District,
                PostalCode = x.PostalCode,

                OrderDetails = x.OrderDetails.Select(y =>
                    new ResultOrderDetailDto
                    {
                        OrderDetailId = y.OrderDetailId,
                        ProductId = y.ProductId,
                        ProductName = y.ProductName,
                        UnitPrice = y.UnitPrice,
                        Quantity = y.Quantity,
                        TotalPrice = y.TotalPrice
                    }).ToList()
            }).ToList();
        }

        public async Task<GetByIdCustomerOrderDto> GetCustomerOrderByIdAsync(
            int customerOrderId)
        {
            var order = await _context.CustomerOrders
                .Include(x => x.OrderDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.CustomerOrderId == customerOrderId);

            if (order == null)
            {
                throw new KeyNotFoundException(
                    "Sipariş bulunamadı.");
            }

            return new GetByIdCustomerOrderDto
            {
                CustomerOrderId = order.CustomerOrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                OrderStatus = order.OrderStatus,
                ReceiverName = order.ReceiverName,
                ReceiverSurname = order.ReceiverSurname,
                PhoneNumber = order.PhoneNumber,
                AddressLine = order.AddressLine,
                City = order.City,
                District = order.District,
                PostalCode = order.PostalCode,

                OrderDetails = order.OrderDetails.Select(x =>
                    new ResultOrderDetailDto
                    {
                        OrderDetailId = x.OrderDetailId,
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        UnitPrice = x.UnitPrice,
                        Quantity = x.Quantity,
                        TotalPrice = x.TotalPrice
                    }).ToList()
            };
        }

        public async Task<List<ResultCustomerOrderDto>>
            GetCustomerOrdersByUserIdAsync(string userId)
        {
            var orders = await _context.CustomerOrders
                .Include(x => x.OrderDetails)
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();

            return orders.Select(x => new ResultCustomerOrderDto
            {
                CustomerOrderId = x.CustomerOrderId,
                UserId = x.UserId,
                OrderDate = x.OrderDate,
                TotalPrice = x.TotalPrice,
                OrderStatus = x.OrderStatus,
                ReceiverName = x.ReceiverName,
                ReceiverSurname = x.ReceiverSurname,
                PhoneNumber = x.PhoneNumber,
                AddressLine = x.AddressLine,
                City = x.City,
                District = x.District,
                PostalCode = x.PostalCode,

                OrderDetails = x.OrderDetails.Select(y =>
                    new ResultOrderDetailDto
                    {
                        OrderDetailId = y.OrderDetailId,
                        ProductId = y.ProductId,
                        ProductName = y.ProductName,
                        UnitPrice = y.UnitPrice,
                        Quantity = y.Quantity,
                        TotalPrice = y.TotalPrice
                    }).ToList()
            }).ToList();
        }

        public async Task CreateCustomerOrderAsync(
            CreateCustomerOrderDto createCustomerOrderDto)
        {
            var orderDetails = createCustomerOrderDto.OrderDetails
                .Select(x => new OrderDetail
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                    TotalPrice = x.UnitPrice * x.Quantity
                }).ToList();

            var customerOrder = new CustomerOrder
            {
                UserId = createCustomerOrderDto.UserId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = orderDetails.Sum(x => x.TotalPrice),
                OrderStatus = string.IsNullOrWhiteSpace(
                    createCustomerOrderDto.OrderStatus)
                    ? "Sipariş Alındı"
                    : createCustomerOrderDto.OrderStatus,

                ReceiverName = createCustomerOrderDto.ReceiverName,
                ReceiverSurname = createCustomerOrderDto.ReceiverSurname,
                PhoneNumber = createCustomerOrderDto.PhoneNumber,
                AddressLine = createCustomerOrderDto.AddressLine,
                City = createCustomerOrderDto.City,
                District = createCustomerOrderDto.District,
                PostalCode = createCustomerOrderDto.PostalCode,
                OrderDetails = orderDetails
            };

            await _context.CustomerOrders.AddAsync(customerOrder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerOrderAsync(
            UpdateCustomerOrderDto updateCustomerOrderDto)
        {
            var order = await _context.CustomerOrders
                .Include(x => x.OrderDetails)
                .FirstOrDefaultAsync(x =>
                    x.CustomerOrderId ==
                    updateCustomerOrderDto.CustomerOrderId);

            if (order == null)
            {
                throw new KeyNotFoundException(
                    "Güncellenecek sipariş bulunamadı.");
            }

            order.UserId = updateCustomerOrderDto.UserId;
            order.OrderStatus = updateCustomerOrderDto.OrderStatus;
            order.ReceiverName = updateCustomerOrderDto.ReceiverName;
            order.ReceiverSurname = updateCustomerOrderDto.ReceiverSurname;
            order.PhoneNumber = updateCustomerOrderDto.PhoneNumber;
            order.AddressLine = updateCustomerOrderDto.AddressLine;
            order.City = updateCustomerOrderDto.City;
            order.District = updateCustomerOrderDto.District;
            order.PostalCode = updateCustomerOrderDto.PostalCode;

            _context.OrderDetails.RemoveRange(order.OrderDetails);

            order.OrderDetails = updateCustomerOrderDto.OrderDetails
                .Select(x => new OrderDetail
                {
                    CustomerOrderId = order.CustomerOrderId,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                    TotalPrice = x.UnitPrice * x.Quantity
                }).ToList();

            order.TotalPrice = order.OrderDetails
                .Sum(x => x.TotalPrice);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerOrderAsync(int customerOrderId)
        {
            var order = await _context.CustomerOrders
                .FirstOrDefaultAsync(x =>
                    x.CustomerOrderId == customerOrderId);

            if (order == null)
            {
                throw new KeyNotFoundException(
                    "Silinecek sipariş bulunamadı.");
            }

            _context.CustomerOrders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
