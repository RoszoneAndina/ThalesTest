using System;
using System.Linq;
using System.Text;
using ThalesDAL.Data;
using ThalesDTO.Models;

namespace ThalesBLL.Services
{

    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetList();
        public Task<ProductDTO?> GeyByID(int id);
    }
    public class ProducService(IEscuelaHttpClient client) : IProductService
    {
        public async Task<ProductDTO?> GeyByID(int id)
        {
            var result = await client.GetAsync<ProductDTO>($"products/{id}");
            var product = result ?? new ProductDTO(); ;
            var productWithTax = ComputeTax(product);
            return productWithTax;
        }

        public async Task<IEnumerable<ProductDTO>> GetList()
        {
            var result = await client.GetAsync<IEnumerable<ProductDTO>>("products");
            var products = result ?? new List<ProductDTO>();
            var productsWithTax = products.Select(m => ComputeTax(m));
            return productsWithTax;
        }

        private static ProductDTO ComputeTax(ProductDTO product)
        {
            var tax = 0.19;
            product.Tax = product.Price * tax;
            return product;
        }
    }
}
