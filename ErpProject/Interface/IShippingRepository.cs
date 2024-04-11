using ErpProject.Models;

namespace ErpProject.Interface
{
    public interface IShippingRepository
    {
        List<ShippingAddress> getAllShippingAddresses();
        ShippingAddress getShippingAddressById(int id);
        ShippingAddress addShippingAddress(ShippingAddress shippingAddress);
        ShippingAddress updateShippingAddress(ShippingAddress shippingAddress);
        void deleteShippingAddress(int id);  
    }
}
