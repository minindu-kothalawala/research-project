using ResearchProject.IRepository;
using ResearchProject.IRepositories;
using ResearchProject.Models;

namespace ResearchProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _repository;

        public UserRepository(IRepository<User> repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> GetAll() => _repository.GetAll();

        public User? GetById(int id) => _repository.GetById(id);

        public void Add(User entity) => _repository.Add(entity);

        public bool Update(User entity) => _repository.Update(entity);

        public bool Delete(int id) => _repository.Delete(id);
    }
}
