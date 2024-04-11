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
        public bool uniqueEmail(string email);
    }
}
