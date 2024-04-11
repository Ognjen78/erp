using ErpProject.Controllers;
using ErpProject.Interface;
using ErpProject.Models;
using System.Security.Cryptography;

namespace ErpProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly static int iterations = 1000;
        public UserRepository(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public User addUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public void deleteUser(Guid id)
        {
            var user = dbContext.Users.Find(id);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
        }

        public List<User> getAllUsers()
        {
            return dbContext.Users.ToList();
        }

        public User getUserById(Guid id)
        {
            return dbContext.Users.Find(id);
        }

        public User updateUser(User user)
        {
            var u = getUserById(user.id_user);
            u.name = user.name;
            u.surname = user.surname;
            u.email = user.email;
            u.username = user.username;
            u.password = user.password;
            u.salt = user.salt;
            dbContext.SaveChanges();
            return user;
        }

        public bool UserWithCredentialsExists(string username, string password)
        {
            User user = dbContext.Users.FirstOrDefault(u => u.username == username);

            if (user == null) { return false; }

            if (VerifyPassword(password, user.password, user.salt))
            {
                return true;
            }

            return false;


        }

        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }

        public bool uniqueEmail(string email)
        {
            var unique = dbContext.Users.Any(u => u.email == email);
            return unique;
        }

    }
}
