using MongoDB.Driver;
using TheShop.Catalog.Dtos.ProductDtos;
using TheShop.Catalog.Entities;
using TheShop.Catalog.Settings;

namespace TheShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _productCollection = database.GetCollection<Product>(
                databaseSettings.ProductCollectionName);
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                ProductName = createProductDto.ProductName,
                Brand = createProductDto.Brand,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                CategoryId = createProductDto.CategoryId
            };

            await _productCollection.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await _productCollection
                .Find(x => true)
                .ToListAsync();

            return values.Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Brand = x.Brand,
                Price = x.Price,
                Stock = x.Stock,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var value = await _productCollection
                .Find(x => x.ProductId == id)
                .FirstOrDefaultAsync();

            if (value == null)
                return null;

            return new GetByIdProductDto
            {
                ProductId = value.ProductId,
                ProductName = value.ProductName,
                Brand = value.Brand,
                Price = value.Price,
                Stock = value.Stock,
                CategoryId = value.CategoryId
            };
        }

        public async Task<List<ResultProductDto>> GetProductsByCategoryIdAsync(string categoryId)
        {
            var values = await _productCollection
                .Find(x => x.CategoryId == categoryId)
                .ToListAsync();

            return values.Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Brand = x.Brand,
                Price = x.Price,
                Stock = x.Stock,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = await _productCollection
                .Find(x => x.ProductId == updateProductDto.ProductId)
                .FirstOrDefaultAsync();

            if (value == null)
                return;

            value.ProductName = updateProductDto.ProductName;
            value.Brand = updateProductDto.Brand;
            value.Price = updateProductDto.Price;
            value.Stock = updateProductDto.Stock;
            value.CategoryId = updateProductDto.CategoryId;

            await _productCollection.ReplaceOneAsync(
                x => x.ProductId == value.ProductId,
                value);
        }
    }
}
