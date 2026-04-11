using ResearchProject.IRepository;
using ResearchProject.IRepositories;
using ResearchProject.Models;

namespace ResearchProject.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<Product> _repository;

        public ProductRepository(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetAll() => _repository.GetAll();

        public Product? GetById(int id) => _repository.GetById(id);

        public void Add(Product entity) => _repository.Add(entity);

        public bool Update(Product entity) => _repository.Update(entity);

        public bool Delete(int id) => _repository.Delete(id);
    }
}
