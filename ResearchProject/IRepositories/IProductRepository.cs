using ResearchProject.IRepositories;
using ResearchProject.Models;

namespace ResearchProject.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        // Product-specific query methods can be added here
    }
}
