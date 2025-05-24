using Microsoft.EntityFrameworkCore;
using RegistrationApp.DataContext;
using RegistrationApp.Models;
using RegistrationApp.Models.CoreModels;

namespace RegistrationApp.ServiceRepository
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext applicationDbContext;
        public ProductService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public bool AddProduct(CreateProductManagerVM productRequest)
        {
            try
            {
                if (productRequest.ImageFile != null && productRequest.ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productRequest.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        productRequest.ImageFile.CopyTo(stream);
                    }
                    productRequest.ImagePath = "/images/" + fileName;
                }
                ProductManager product = new ProductManager()
                {
                    ProductId = Guid.NewGuid(),
                    ImagePath= productRequest.ImagePath,
                    ProductName=productRequest.ProductName,
                    Description=productRequest.Description,
                    DateAdded = DateTime.Now,
                    UserId=productRequest.UserId
                };
                applicationDbContext.Add(product);
                applicationDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return false;
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                var product = applicationDbContext.Products.Find(id);
                if (product != null)
                {
                    applicationDbContext.Products.Remove(product);
                    applicationDbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return false;
        }

        public ProductManager GetProductById(Guid id)
        {
            ProductManager response = new();
            try
            {
                response = applicationDbContext.Products.Where(r => r.ProductId == id).FirstOrDefault()!;
                if (response != null)
                    return response;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return response!;
        }

        public IEnumerable<ProductManager> GetProductsByUserId(Guid id)
        {
            try
            {
                IEnumerable<ProductManager> products = applicationDbContext.Products.Where(p=>p.UserId==id).ToList()!;
                if (products != null)
                    return products;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Enumerable.Empty<ProductManager>();
        }

        public bool UpdateProduct(UpdateProductManagerVM updateProductRequest)
        {
            try
            {
                ProductManager product = applicationDbContext.Products.Where(r => r.ProductId == updateProductRequest.ProductId).FirstOrDefault()!;
                if (product != null)
                {
                    if (updateProductRequest.ImageFile != null && updateProductRequest.ImageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateProductRequest.ImageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            updateProductRequest.ImageFile.CopyTo(stream);
                        }
                        updateProductRequest.ImagePath = "/images/" + fileName;
                    }

                    product.ProductName = updateProductRequest.ProductName;
                    product.Description = updateProductRequest.Description;
                    product.ImagePath = updateProductRequest.ImagePath;

                    applicationDbContext.Products.Update(product);
                    applicationDbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return false!;
        }
    }
}
