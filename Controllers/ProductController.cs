using Microsoft.AspNetCore.Mvc;
using ProductManagementApp.Interfaces;
using ProductManagementApp.Models;

namespace ProductManagementApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index() => View(await _productService.GetAllProductsAsync());
        public async Task<IActionResult> Details(int id) => View(await _productService.GetProductByIdAsync(id));
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id) => View(await _productService.GetProductByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Report()
        {
            ViewBag.AveragePriceByCategory = await _productService.GetAveragePriceByCategoryAsync();
            ViewBag.HighestStockCategory = await _productService.GetHighestStockCategoryAsync();
            return View();
        }
    }
}
