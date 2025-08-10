using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.domain.entities;
using Ecommerce.domain.Specification.Cartspec;

namespace Ecommerce.infrastructure.services
{
    public class CartService : IcartService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public CartService(IUnitofwork unitofwork , IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<DefaultserviceResponse> Add(string userid,CartDto dto)
        {
            var product =await _unitofwork.ProductRepository.Get(dto.ProductId);
            if (product == null)
            {
                return new DefaultserviceResponse
                {
                    Success = false,
                    Message = "Product Not Found",
                    Type = "Not Found"
                };
            }

            if (product.Stock< dto.Quantity)
               {
                return new DefaultserviceResponse
                {
                    Message = "Not Have Enough quantity to add to cart",
                    Success =false,
                    Type ="bad request"
                };
            }
            var spec = new existingCart(dto.ProductId, userid);
            var cart = await _unitofwork.CartRepository.FindAsync(spec);
            if (cart != null)
            {
                cart.UpdatedAt = DateTime.Now;
                cart.Quantity += dto.Quantity;
                _unitofwork.CartRepository.Update(cart);
            }
            else
            {
               var newcart =  _mapper.Map<Cart>(dto);
                newcart.UserId=userid;
               await _unitofwork.CartRepository.Add(newcart);
            }
            await _unitofwork.CompleteAsync();
            return new DefaultserviceResponse
            {
                Success = true,
                Message = "Cart Added Successfully"
            };
        }

        public async Task<DefaultserviceResponse>? Delete(string Userid , int productid)
        {
           var spec = new existingCart(productid, Userid);
            var cart =await _unitofwork.CartRepository.FindAsync(spec);
            if (cart == null)
            {
                return new DefaultserviceResponse
                {
                    Message = "Cart Not Found",
                    Success = false,
                    Type = "Not Found"
                };
            }
           await _unitofwork.CartRepository.Delete(cart.Id);
           await _unitofwork.CompleteAsync();
            return new DefaultserviceResponse
            {
                Success = true,
                Message = "deleted Sucessfully"
            };
        }


        public async Task<IEnumerable<UserCartResponseDto>> GetUserCart(string userId)
        {
            var spec = new CartWithProductSpecification(userId);
            var cartitems  =await _unitofwork.CartRepository.GetAllAsync(spec);
            return _mapper.Map<IEnumerable<UserCartResponseDto>> (cartitems);
        }

        public async Task<DefaultserviceResponse> update(string UserId,int productId , UpdateCartDto dto)
        {
            var product = await _unitofwork.ProductRepository.Get(productId);
            if ( product == null)
            {
                return new DefaultserviceResponse
                {
                    Success = false,
                    Message = "Product NotFound",
                    Type= "Not Found"

                };
            }
            var spec = new existingCart(productId, UserId);
            var cart =await _unitofwork.CartRepository.FindAsync(spec);
            if (cart == null)
            {
                return new DefaultserviceResponse
                {
                    Success = false,
                    Message = "Cart NotFound",
                    Type = "Not Found"

                };
            }
            _mapper.Map(dto,cart);
              if (cart.Quantity>product.Stock)
            {
                return new DefaultserviceResponse
                {
                    Success = false,
                    Message = "cart quantity have more quantity than stock",
                    Type = "bad request"
                };
            }
             _unitofwork.CartRepository.Update(cart);
            await _unitofwork.CompleteAsync();
            return new DefaultserviceResponse
            {
                Success = true,
                Message = "Updated SuccessFully"
            };
        }
    }
}
