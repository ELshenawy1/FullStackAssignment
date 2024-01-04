using API.Helpers;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Settings;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string imagesPath;


        public ProductController(IUnitOfWork _unitOfWork, 
                                 IMapper _mapper, 
                                 IWebHostEnvironment _webHostEnvironment)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            webHostEnvironment = _webHostEnvironment;
            imagesPath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        [HttpGet]
        //[Authorize]   
        public ActionResult<Pagination<ProductToReturnDTO>> GetAll([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductSpecifications(productParams);
            var countSpec = new ProductForCountSpecification(productParams);
            var totalItems = unitOfWork.Repository<Product>().Count(countSpec);
            var products = unitOfWork.Repository<Product>().List(spec);
            var data = mapper.Map<List<Product>, List<ProductToReturnDTO>>(products);
            return Ok(new Pagination<ProductToReturnDTO>(productParams.PageIndex,productParams.PageSize,totalItems,data));
        }

        [HttpGet("{id}")]
        public ActionResult<ProductToReturnDTO> GetByID(string id)
        {
            var product = unitOfWork.Repository<Product>().GetByID(id);
            return Ok(mapper.Map<Product,ProductToReturnDTO>(product));
        }


        [HttpPost]
        public async Task<ActionResult<Product>> Create ([FromForm] ProductToAddDTO productModel)
        {
            if (ModelState.IsValid)
            {
                var imgName = productModel.Img.FileName;
                var path = Path.Combine(imagesPath, imgName);

                using var stream = System.IO.File.Create(path);
                await productModel.Img.CopyToAsync(stream);

                var product = mapper.Map<ProductToAddDTO, Product>(productModel);
                unitOfWork.Repository<Product>().Add(product);
                unitOfWork.Complete();
                return Ok(product);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductEditDTO productModel)
        {
            if (ModelState.IsValid)
            {
                var product = unitOfWork.Repository<Product>().GetByID(productModel.ProductCode);
                if(productModel.Img is not null)
                {
                    var imgName = productModel.Img?.FileName;
                    var path = Path.Combine(imagesPath, imgName);

                    using var stream = System.IO.File.Create(path);
                    await productModel.Img.CopyToAsync(stream);
                    product.Img = productModel.Img.FileName;
                }

                product.Name = productModel.Name;
                product.Price = productModel.Price;
                product.Category = productModel.Category;
                product.MinQuantity = productModel.MinQuantity;
                product.DiscountRate = productModel.DiscountRate;
                unitOfWork.Repository<Product>().Update(product);
                unitOfWork.Complete();
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete()]
        public IActionResult Delete(string productCode)
        {
            unitOfWork.Repository<Product>().Delete(unitOfWork.Repository<Product>().GetByID(productCode));
            unitOfWork.Complete();
            return Ok();
        }
    }
}
