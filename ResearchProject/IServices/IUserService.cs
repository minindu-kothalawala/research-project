using ResearchProject.Models;

namespace ResearchProject.IServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(int id);
        User CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
