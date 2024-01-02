using API.Helpers;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hostingEnvironment;


        public ProductController(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _hostingEnvironment)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            hostingEnvironment = _hostingEnvironment;
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
        public async Task<ActionResult<Product>> Create([FromForm]ProductToAddDTO productModel)
        {
            if (ModelState.IsValid)
            {
                var product = mapper.Map<ProductToAddDTO, Product>(productModel);
                unitOfWork.Repository<Product>().Add(product);
                unitOfWork.Complete();

                // Save the image to the wwwroot/images folder
                string wwwrootPath = hostingEnvironment.WebRootPath;
                string imagesFolderPath = Path.Combine(wwwrootPath, "images");

                // Ensure the images folder exists
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                // Save the image
                string imagePath = Path.Combine(imagesFolderPath, productModel.Img.FileName);

                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await productModel.Img.CopyToAsync(fileStream);
                }
                return Ok(product);
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
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
