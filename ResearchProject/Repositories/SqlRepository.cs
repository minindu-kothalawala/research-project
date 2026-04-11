using ResearchProject.Data;
using ResearchProject.IRepositories;
using ResearchProject.Models;

namespace ResearchProject.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SqlDbContext _context;

        public SqlRepository(SqlDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public T? GetById(int id) => _context.Set<T>().Find(id);

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            var existing = GetById(entity.Id);
            if (existing is null) return false;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var entity = GetById(id);
            if (entity is null) return false;

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
