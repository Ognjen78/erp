using ErpProject.Controllers;
using ErpProject.Interface;
using ErpProject.Models;
using System.Security.Cryptography;

namespace ErpProject.Repository
{
    public class AdminRepository : IAdminRepository
    {

        private readonly ApplicationDbContext dbContext;
        private readonly static int iterations = 1000;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Admin> getAllAdmins()
        {
            return dbContext.Admins.ToList();
        }
        public Admin getAdminById(Guid id)
        {
            return dbContext.Admins.Find(id);
        }
        public Admin addAdmin(Admin admin)
        {
            dbContext.Admins.Add(admin);
            dbContext.SaveChanges();
            return admin;
        }
        public Admin updateAdmin(Admin admin)
        {
            var admins = getAdminById(admin.id_admin);
            admins.email = admin.email;
            admins.password = admin.password;
            admins.username = admin.username;
            dbContext.SaveChanges();
            return admin;
        }
        public void deleteAdmin(Guid id)
        {
            var admin = dbContext.Admins.Find(id);
            if (admin != null)
            {
                dbContext.Admins.Remove(admin);
                dbContext.SaveChanges();
            }

        }

        public bool AdminUsername(string username)
        {
            return dbContext.Admins.Any(u => u.username ==  username);
            
        }

        public bool uniqueEmail(string email)
        {
            return dbContext.Admins.Any(u => u.email == email);
           
        }

        public bool AdminWithCredentialsExists(string username, string password)
        {
            Admin admin = dbContext.Admins.FirstOrDefault(u => u.username == username);

            if (admin == null) { return false; }

            if (VerifyPassword(password, admin.password, admin.salt))
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


    }
}
