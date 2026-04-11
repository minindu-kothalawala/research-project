using ResearchProject.Models;

namespace ResearchProject.IRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
