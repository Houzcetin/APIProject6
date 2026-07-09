using APIProject6.WebUI.Dtos.CategoryDtos;
using APIProject6.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace APIProject6.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProductList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7277/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]

        public async Task <IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7277/api/Categories");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> categoryValues = (from x in values
                                            select new SelectListItem
                                            {
                                                Text=x.CategoryName,
                                                Value=x.CategoryId.ToString()
                                            }).ToList();
            ViewBag.CategoryValues = categoryValues;
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7277/api/Products/CreateProductWithCategory", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7277/api/Products?id=" + id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var categoryResponse = await client.GetAsync("https://localhost:7277/api/Categories");
            var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryJson);
            ViewBag.CategoryValues = (from x in categories
                                      select new SelectListItem
                                      {
                                          Text = x.CategoryName,
                                          Value = x.CategoryId.ToString()
                                      }).ToList();

            var responseMessage = await client.GetAsync("https://localhost:7277/api/Products/GetProduct?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);
            return View(value);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7277/api/Products/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            ViewBag.Error = await responseMessage.Content.ReadAsStringAsync();

            var categoryResponse = await client.GetAsync("https://localhost:7277/api/Categories");
            var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryJson);
            ViewBag.CategoryValues = (from x in categories
                                      select new SelectListItem
                                      {
                                          Text = x.CategoryName,
                                          Value = x.CategoryId.ToString()
                                      }).ToList();

            var model = new GetProductByIdDto
            {
                ProductId = updateProductDto.ProductId,
                ProductName = updateProductDto.ProductName,
                ProductDescription = updateProductDto.ProductDescription,
                Price = updateProductDto.Price,
                ImageURL = updateProductDto.ImageURL,
                CategoryId = updateProductDto.CategoryId
            };
            return View(model);
        }
    }
}
