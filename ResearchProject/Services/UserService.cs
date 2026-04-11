using ResearchProject.IRepositories;
using ResearchProject.IServices;
using ResearchProject.Models;

namespace ResearchProject.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> GetAllUsers() => _repository.GetAll();

        public User? GetUserById(int id) => _repository.GetById(id);

        public User CreateUser(User user)
        {
            _repository.Add(user);
            return user;
        }

        public bool UpdateUser(User user) => _repository.Update(user);

        public bool DeleteUser(int id) => _repository.Delete(id);
    }
}