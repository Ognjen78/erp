using ErpProject.Controllers;
using ErpProject.Interface;
using ErpProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace ErpProject.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Product addProduct(Product product)
        {
            dbContext.Products.Add(product);    
            dbContext.SaveChanges();
            return product;
        }

        public void deleteProduct(int id)
        {
            var product = dbContext.Products.Find(id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
            }
           
        }

        public List<Product> getAllProducts()
        {
            return dbContext.Products.ToList();
        }

        public Product getProductById(int id)
        {
            return dbContext.Products.Find(id);
        }

        public Product updateProduct(Product product)
        {
            Product updateProduct = getProductById(product.id_product);
            updateProduct.name = product.name;
            updateProduct.price = product.price;
            updateProduct.brand = product.brand;
            updateProduct.category = product.category;
            updateProduct.description = product.description;
            updateProduct.stock = product.stock;
            updateProduct.gender = product.gender;
            updateProduct.imageUrl = product.imageUrl;
            dbContext.SaveChanges();
            return product;
        }

       /* public async Task<List<Product>> search(string searchTerm)
        {
            var normalize = searchTerm.ToLower();
            var result = await dbContext.Products
                .Where(u => u.category.ToLower().Contains(normalize) || u.name.ToLower().Contains(normalize)).ToListAsync();
            return result;
        }*/

       /* public List<Product> sort(bool asc)
        {
            var result = getAllProducts();

            if (asc)
            {
                var sorted = result.OrderBy(u => u.price).ToList();
                return sorted;
            }
            else
            {
                var sorted = result.OrderByDescending(u => u.price).ToList();
                return sorted;
            } 
              
        } */

       /* public List<Product> getAllProducts(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return dbContext.Products.Skip(skip).Take(pageSize).ToList();
        }
       */
    }
}
