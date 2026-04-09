using Microsoft.EntityFrameworkCore;
using ResearchProject.Data;
using ResearchProject.Models;

namespace ResearchProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll() => _context.Products.ToList();

        public Product? GetById(int id) => _context.Products.Find(id);

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public bool Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing is null) return false;

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product is null) return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}
