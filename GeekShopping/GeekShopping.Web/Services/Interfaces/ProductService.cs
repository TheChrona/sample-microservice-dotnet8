using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> FindAllProducts()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<IEnumerable<ProductViewModel>>();
       }

        public async Task<ProductViewModel> FindProductById(long id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductViewModel>();
        }
        public async Task<ProductViewModel> CreateProduct(ProductViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(BasePath, model);
            if(response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductViewModel>();
            } else
            {
                throw new Exception("Something went wrong when calling API");
            }
        }
        public async Task<ProductViewModel> UpdateProduct(ProductViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync(BasePath, model);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductViewModel>();
            }
            else
            {
                throw new Exception("Something went wrong when calling API");
            }
        }

        public async Task<bool> DeleteProductById(long id)
        {
            var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<bool>();
        }
    }
}
