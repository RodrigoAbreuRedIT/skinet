using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications {
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product> {
        public ProductsWithTypesAndBrandsSpecification(ProductsSpecParams productParams) :
            base(x => (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                      (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && // Estamos a ir buscar os produtos com um determinado BrandID 
                      (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)) { // Estamos a ir buscar os produtos com um determinado TypeID

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort)) {
                switch (productParams.Sort) {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }

        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id) {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}