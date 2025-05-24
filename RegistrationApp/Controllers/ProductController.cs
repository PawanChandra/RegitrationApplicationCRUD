using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistrationApp.Models;
using RegistrationApp.Models.CoreModels;
using RegistrationApp.ServiceRepository;

namespace RegistrationApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
            if (userId != Guid.Empty)
            {
                var products = _productService.GetProductsByUserId(userId) ?? Enumerable.Empty<ProductManager>();
                return View(products);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductManagerVM productRequest)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                productRequest.UserId = Guid.Parse(userIdString);
                bool res = _productService.AddProduct(productRequest);
                if (res)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            return View(productRequest);
        }

        public IActionResult Edit(Guid id)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
            if(id != Guid.Empty)
            {
                var product = _productService.GetProductById(id);
                UpdateProductManagerVM updateProductVM = new UpdateProductManagerVM
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    ImagePath=product.ImagePath
                };
                return View(updateProductVM);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(UpdateProductManagerVM product)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
            if (product.ProductId !=Guid.Empty)
            {
                try
                {
                    bool res = _productService.UpdateProduct(product);
                    if (res)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return RedirectToAction("Index","Product");
            }
            return View(product);
        }
        public IActionResult Delete(Guid id)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id != Guid.Empty)
            {
                bool res = _productService.DeleteProduct(id);
                if (res)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
