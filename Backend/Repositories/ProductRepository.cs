using Backend.Models;
using Backend.Services;

namespace Backend.Repositories
{
    public interface IProductRepository
    {
        Task<string> AddPoductToDatabase(Product product);
        List<Product> GetAll();
        Task<Product> GetProductById(string id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDBService<Product> _mongoService;
        const string collectionName = "products";
        public ProductRepository(IMongoDBService<Product> mongoService)
        {
            _mongoService = mongoService;
        }
        public async Task<string> AddPoductToDatabase(Product product)
        {
            return await _mongoService.AddToDatabase(product,collectionName);
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _mongoService.GetItemById(collectionName,id);
        }
        
        public List<Product> GetAll()
        {
            return _mongoService.GetAll(collectionName);
        }
    }
}
