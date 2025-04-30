using eCommerce_dpei.Models;

namespace eCommerce_dpei.Services
{
    public interface IProductImageRepository
    {
        Task Add(int productId, List<IFormFile> image);
        Task<bool> Remove(int productid);
    }
}
