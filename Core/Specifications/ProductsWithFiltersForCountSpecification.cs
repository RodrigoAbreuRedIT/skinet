using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product> {
        public ProductsWithFiltersForCountSpecification(ProductsSpecParams productParams) : 
                  base(x => (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && // Estamos a ir buscar os produtos com um determinado BrandID 
                            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)) { // Estamos a ir buscar os produtos com um determinado TypeID
        }
    }
}