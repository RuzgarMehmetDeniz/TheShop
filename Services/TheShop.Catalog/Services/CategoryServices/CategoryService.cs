using MongoDB.Driver;
using TheShop.Catalog.Dtos.CategoryDtos;
using TheShop.Catalog.Entities;
using TheShop.Catalog.Settings;

namespace TheShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                CategoryName = createCategoryDto.CategoryName
            };

            await _categoryCollection.InsertOneAsync(category);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values = await _categoryCollection
                .Find(x => true)
                .ToListAsync();

            return values.Select(x => new ResultCategoryDto
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            }).ToList();
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var value = await _categoryCollection
                .Find(x => x.CategoryId == id)
                .FirstOrDefaultAsync();

            if (value == null)
                return null;

            return new GetByIdCategoryDto
            {
                CategoryId = value.CategoryId,
                CategoryName = value.CategoryName
            };
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = await _categoryCollection
                .Find(x => x.CategoryId == updateCategoryDto.CategoryId)
                .FirstOrDefaultAsync();

            if (value == null)
                return;

            value.CategoryName = updateCategoryDto.CategoryName;

            await _categoryCollection.ReplaceOneAsync(
                x => x.CategoryId == value.CategoryId,
                value);
        }
    }
}
