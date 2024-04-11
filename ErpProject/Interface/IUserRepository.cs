using ErpProject.Models;

namespace ErpProject.Interface
{
    public interface IUserRepository
    {
        List<User> getAllUsers();
        User getUserById(Guid id);
        User addUser(User user);
        User updateUser(User user);
        void deleteUser(Guid id);
        public bool UserWithCredentialsExists(string email, string password);
        public bool uniqueEmail(string email);
    }
}
