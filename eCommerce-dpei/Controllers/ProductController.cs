using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace ecommerce_dpei.controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class Productcontroller : ControllerBase
    {
        private readonly IproductService _service;
        public Productcontroller(IproductService Service)
        {
            _service = Service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getproduct(int id)
        {

           var product= await _service.Get(id);

            if (product == null) 
                return NotFound(new {message= "Product Not Found" });
            return Ok(product);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> createproduct([FromForm] ProductDto dto)
        {
                 await _service.Add(dto);
                 return Ok("Added Successfully");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateproduct(int id, [FromBody] Updateproductdto dto)
        {
        
             var result= await _service.update(id, dto);
            if (!result.Success)
                return NotFound(new { Message = result.Message });

            return Ok(new { Message = result.Message });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =await _service.Delete(id);
            if (!result.Success)
                return NotFound(new { message = result.Message });
            return Ok(new {message = result.Message});
        }
        [HttpPost("{id}")]        
        public async Task<IActionResult> AddProductImage(int id,AddImageDto dto)
        {
            var result =await _service.Addimage(id, dto);
            if(!result.Success)
                return NotFound(new {message = result.Message});
            return Ok(new {message = result.Message});
        }

        [HttpDelete("{productid}/Image/{imageid}")]
        public async Task<IActionResult> DeleteImage(int productid,int imageid)
        {
            var result = await _service.DeleteImage(productid, imageid);
            if (!result.Success)
                return NotFound(new { message = result.Message });
            return Ok(new {message = result.Message});
        }
    }
}