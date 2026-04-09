using ResearchProject.Models;

namespace ResearchProject.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }
}
