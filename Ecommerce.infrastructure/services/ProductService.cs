using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.domain.entities;
using Ecommerce.domain.Specification.products;
using FluentValidation;


namespace Ecommerce.infrastructure.services
{
    public class ProductService : IproductService
    {
        private readonly IValidator<ProductDto> _validator;
        private readonly IValidator<Updateproductdto> _productvalidator;
        private readonly IValidator<AddImageDto> _addimagevalidator;
        private readonly IMapper _mapper;
        private readonly IUnitofwork _unitofwork;
        public ProductService(IValidator<ProductDto> validator, IMapper Mapper
            , IUnitofwork Unitofwork, IValidator<Updateproductdto> productvalidator
            , IValidator<AddImageDto> addimagevalidator)
        {
            _validator = validator;
            _mapper = Mapper;
            _unitofwork = Unitofwork;
            _productvalidator = productvalidator;
            _addimagevalidator = addimagevalidator;
        }

        public async Task Add(ProductDto dto)
        {
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var product = _mapper.Map<Product>(dto);
            bool IsFirst = true;
            product.ProductImage = new List<ProductImage>();
            foreach (var image in dto.ProductImages)
            {
                var filename = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                var savepath = Path.Combine("wwwroot/images/products", filename);
                var directory = Path.GetDirectoryName(savepath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                using (var stream = new FileStream(savepath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                var productimage = new ProductImage
                {
                   
                    ImageUrl = $"/images/products/{filename}",
                    IsPrimary =IsFirst,
                };
                IsFirst=false;
                product.ProductImage.Add(productimage);

            }

            await _unitofwork.ProductRepository.Add(product);
            await _unitofwork.CompleteAsync();

        }

        public async Task<Product>? Get(int id)
        { var spec =new  PreductWithimageSpecification(id);
           var existproduct=await _unitofwork.ProductRepository.FindAsync(spec);
            if (existproduct == null)
                return null;
            return existproduct;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
           return await _unitofwork.ProductRepository.GetAll();
        }

        public async Task<DefaultserviceResponse> update(int id, Updateproductdto dto)
        {
            var result = await _productvalidator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            
            var existproduct =await _unitofwork.ProductRepository.Get(id);
            if (existproduct == null)
                return new DefaultserviceResponse
                {
                    Success = false,
                    Message = "Product Not Found"
                };

            var existcategory =await _unitofwork.CategoryRepository.Get(dto.CategoryId);
            if(existcategory == null)
                return new DefaultserviceResponse
                {
                    Success = false,
                    Message = "Category Not Found"
                };
             _mapper.Map(dto,existproduct);
             _unitofwork.ProductRepository.Update(existproduct);
            existproduct.UpdatedAt = DateTime.Now;
            await _unitofwork.CompleteAsync();
            return new DefaultserviceResponse
            {   Success= true,
                Message = "updateed Successfully"
            };
        }

        public async Task<DefaultserviceResponse>? Delete(int id)
        {
            var spec = new PreductWithimageSpecification(id);
            var existproduct =await _unitofwork.ProductRepository?.FindAsync(spec);
            if (existproduct == null)
            {
                return new DefaultserviceResponse
                {  Success =false,
                   Message = "product Not found"
                };
            }
            foreach (var image in existproduct.ProductImage)
            {
                var filePath = Path.Combine("wwwroot", image.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                await _unitofwork.ProductImageRepository.Delete(image.Id);
            }
            await _unitofwork.ProductRepository.Delete(id);
            await _unitofwork.CompleteAsync();
            return new DefaultserviceResponse
            {
                Success = true,
                Message = " deleted Successfully"
            };
        }

        public async Task<DefaultserviceResponse> Addimage(int ProductId ,AddImageDto dto )
        {
            var result = await _addimagevalidator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var existproduct = await _unitofwork.ProductRepository.Get(ProductId);
            if (existproduct == null)
            {
                return new DefaultserviceResponse
                {
                    Success =false,
                    Message = $"Product with ID :{ProductId} npot found"
                };
            }
            if (dto.IsPrimary)
            {
                var existingImages = await _unitofwork.ProductImageRepo.GetImagesByProductId(ProductId);
                foreach (var img in existingImages.Where(x => x.IsPrimary))
                {
                    img.IsPrimary = false;
                    _unitofwork.ProductImageRepository.Update(img);
                }
            }

            var filename = $"{Guid.NewGuid()}_{Path.GetFileName(dto.Image.FileName)}";
            var savepath = Path.Combine("wwwroot/images/products", filename);
            var directory = Path.GetDirectoryName(savepath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            using (var stream = new FileStream(savepath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }
            var productimage = new ProductImage
            {
                ProductId = ProductId,
                ImageUrl = $"/images/products/{filename}",
                IsPrimary = dto.IsPrimary,
            };
            
           await _unitofwork.ProductImageRepository.Add(productimage);
            await _unitofwork.CompleteAsync();
            return new DefaultserviceResponse
            {
                Success = true,
                Message = "Added Successfully"
            };
        }

        public async Task<DefaultserviceResponse> DeleteImage(int productId, int ImageId)
        {
            var existproduct =await _unitofwork.ProductRepository.Get(productId); 
            if (existproduct == null)
            {
                return new DefaultserviceResponse
                {
                    Success = false,
                    Message = "product not found"
                };
            }
            var existimage = await _unitofwork.ProductImageRepository.Get(ImageId);
            if (existimage == null || existimage.ProductId !=productId)
            {
                return new DefaultserviceResponse
                {
                    Success = false,
                    Message = "Image not found or doesnnot belelong to specified product"
                };
            }

            var filePath = Path.Combine("wwwroot", existimage.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            await _unitofwork.ProductImageRepository.Delete(ImageId);
            await _unitofwork.CompleteAsync();
            return new DefaultserviceResponse
            {
                Success = true,
                Message = "Image deleted successfully"
            };
        }
   
    }
}
