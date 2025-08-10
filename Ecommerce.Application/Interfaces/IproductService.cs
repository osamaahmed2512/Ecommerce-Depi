using Ecommerce.Application.Dto;
using Ecommerce.domain.entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IproductService
    {
        Task<Product>? Get(int id);
        Task<IEnumerable<Product>> GetAll();
        Task Add(ProductDto dto);
        Task<DefaultserviceResponse> Addimage(int ProductId, AddImageDto dto);

        Task<DefaultserviceResponse> update(int id, Updateproductdto dto);
        Task<DefaultserviceResponse>? Delete(int id);
        Task<DefaultserviceResponse> DeleteImage(int productId, int ImageId);
    }
}
