using ErpProject.Models;

namespace ErpProject.Interface
{
    public interface IProductRepository
    {
        List<Product> getAllProducts();
        Product getProductById(int id);
        Product addProduct(Product product);    
        Product updateProduct(Product product);
        void deleteProduct(int id);

        Task<List<Product>> search(string searchTerm);
    }
}
