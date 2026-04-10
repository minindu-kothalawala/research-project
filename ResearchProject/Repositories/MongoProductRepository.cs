using MongoDB.Driver;
using ResearchProject.Data;
using ResearchProject.Models;

namespace ResearchProject.Repositories
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly MongoDbContext _context;

        public MongoProductRepository(MongoDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll() =>
            _context.Products.Find(_ => true).ToList();

        public Product? GetById(int id) =>
            _context.Products.Find(p => p.Id == id).FirstOrDefault();

        public void Add(Product product)
        {
            var last = _context.Products
                .Find(_ => true)
                .SortByDescending(p => p.Id)
                .Limit(1)
                .FirstOrDefault();

            product.Id = (last?.Id ?? 0) + 1;
            _context.Products.InsertOne(product);
        }

        public bool Update(Product product)
        {
            var result = _context.Products.ReplaceOne(p => p.Id == product.Id, product);
            return result.ModifiedCount > 0;
        }

        public bool Delete(int id)
        {
            var result = _context.Products.DeleteOne(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
