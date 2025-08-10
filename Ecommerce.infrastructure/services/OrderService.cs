using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.domain.entities;
using Ecommerce.domain.Specification.Cartspec;
using Ecommerce.domain.Specification.Orderspec;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.infrastructure.services
{
    public class OrderService : IOrderService

    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public OrderService(IMapper Mapper,IUnitofwork Unitofwork)
        {
            _mapper = Mapper;
            _unitofwork = Unitofwork;
        }
        public async Task<DefaultServiceResponseWithData<Order>> CreateOrderAsync(string userId, int AddressId)
        {
            var existorder =await _unitofwork.OrderRepository.Get(AddressId);
            if (existorder == null)
            {
                return DefaultServiceResponseWithData<Order>.Fail("Address Not Fund ",StatusCodes.Status404NotFound);
            }
            var cartspec = new CartWithProductSpecification(userId);
            var CartItems =await _unitofwork.CartRepository.GetAllAsync(cartspec);
            if (CartItems ==null)
            {
                return DefaultServiceResponseWithData<Order>.Fail("Cart is empty", 400);
            }
            var orderItems = new List<OrderItem>();
            foreach (var CartItem in CartItems)
            {
                var product = CartItem.Product;
                if (product.Stock < CartItem.Quantity)
                {
                    return DefaultServiceResponseWithData<Order>.Fail($"Not enough stock for product: {product.Name}", 400);
                }
                product.Stock -= CartItem.Quantity;
                _unitofwork.ProductRepository.Update(product);
                orderItems.Add(new OrderItem
                {
                    ProductId = CartItem.ProductId,
                    Quantity = CartItem.Quantity,
                    UnitPrice = product.Price
                });
            }

            var order = new Order
            {
                UserId = userId,
                AddressId = AddressId,
                OrderItems = orderItems,
                TotalPrice=orderItems.Sum(i => i.Quantity * i.UnitPrice)
            };


            await _unitofwork.OrderRepository.Add(order);
            _unitofwork.CartRepository.DeleteRange(CartItems);
            await _unitofwork.CompleteAsync();

            return DefaultServiceResponseWithData<Order>.Ok(order,"Added SuccessFully");
        }

        public async Task<Order>? GetOrderByIdAsync(string userId,int orderId)
        {
            var spec = new GetOrderByUserIdandorderId(userId,orderId);
            var order = await _unitofwork.OrderRepository.FindAsync(spec);
            if (order == null)
                return null;
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId)
        {
            var spec = new GetOrderByUserIdspec(customerId);
            return await _unitofwork.OrderRepository.GetAllAsync(spec);
        }

        public async Task<DefaultserviceResponse> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _unitofwork.OrderRepository.Get(orderId);
            if (order == null) 
                return DefaultserviceResponse.Fail("Order Not Found" , StatusCodes.Status404NotFound);
            order.Status = status;
             _unitofwork.OrderRepository.Update(order);
            await _unitofwork.CompleteAsync();
            return DefaultserviceResponse.Ok("Updated Successfully");
        }
    }
}
