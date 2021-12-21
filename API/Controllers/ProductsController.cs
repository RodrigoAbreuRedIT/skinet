using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase {

        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productsBrandRepo;
        private readonly IGenericRepository<ProductType> _productsTypeRepo;
        private readonly IMapper _mapper;


        public ProductsController(IGenericRepository<Product> products,
                                  IGenericRepository<ProductBrand> productsBrand,
                                  IGenericRepository<ProductType> productsType,
                                  IMapper mapper) {

            this._productsRepo = products;
            this._productsBrandRepo = productsBrand;
            this._productsTypeRepo = productsType;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts() {

            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await this._productsRepo.ListAsync(spec);

            return Ok(this._mapper
                   .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) {

            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await this._productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() {
            return Ok(await this._productsBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() {
            return Ok(await this._productsTypeRepo.ListAllAsync());
        }
    }
}