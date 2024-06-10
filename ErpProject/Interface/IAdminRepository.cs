using ErpProject.Models;

namespace ErpProject.Interface
{
    public interface IAdminRepository
    {
        List<Admin> getAllAdmins();
        Admin getAdminById(Guid id);
        Admin addAdmin(Admin admin);
        Admin updateAdmin(Admin admin);
        void deleteAdmin(Guid id);

        public bool AdminUsername(string username);
        public bool uniqueEmail(string email);

        public bool AdminWithCredentialsExists(string username, string password);
        Admin GetAdminByUsername(string username);
    }
}
