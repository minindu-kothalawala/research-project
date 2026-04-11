using ResearchProject.IRepositories;
using ResearchProject.IServices;
using ResearchProject.Models;

namespace ResearchProject.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetAllProducts() => _repository.GetAll();

        public Product? GetProductById(int id) => _repository.GetById(id);

        public Product CreateProduct(Product product)
        {
            _repository.Add(product);
            return product;
        }

        public bool UpdateProduct(Product product) => _repository.Update(product);

        public bool DeleteProduct(int id) => _repository.Delete(id);
    }
}
