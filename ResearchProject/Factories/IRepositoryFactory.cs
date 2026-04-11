using ResearchProject.IRepositories;
using ResearchProject.Models;

namespace ResearchProject.Factories
{
    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>() where T : BaseEntity;
    }
}
