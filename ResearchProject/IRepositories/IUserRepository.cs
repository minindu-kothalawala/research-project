using ResearchProject.IRepositories;
using ResearchProject.Models;

namespace ResearchProject.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        // User-specific query methods can be added here
    }
}
