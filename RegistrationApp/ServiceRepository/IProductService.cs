using RegistrationApp.Models.CoreModels;
using RegistrationApp.Models;

namespace RegistrationApp.ServiceRepository
{
    public interface IProductService
    {
        bool AddProduct(CreateProductManagerVM productRequest);
        IEnumerable<ProductManager> GetProductsByUserId(Guid id);
        ProductManager GetProductById(Guid id);
        bool UpdateProduct(UpdateProductManagerVM updateProductRequest);
        bool DeleteProduct(Guid id);
    }
}
