using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class ProductRepository : IProductRepository {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context) {
            this._context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id) {
            return await this._context.Products
            // Inclui os tipos de produtos e as marcas na pesquisa feita para
            // Sem o Include, nao veriamos as chaves estrangeiras
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync() {
            
            return await this._context.Products
                                        .Include(p => p.ProductType)
                                        .Include(p => p.ProductBrand)
                                        .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync() {
            return await this._context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductsTypesAsync() {
            return await this._context.ProductTypes.ToListAsync();
        }
    }
}