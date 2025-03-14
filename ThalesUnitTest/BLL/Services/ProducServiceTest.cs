using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThalesBLL.Services;
using ThalesDAL.Data;
using ThalesDTO.Models;

namespace ThalesUnitTest.BLL.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IEscuelaHttpClient> _mockHttpClient = null!;
        private IProductService _productService = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockHttpClient = new Mock<IEscuelaHttpClient>();
            _productService = new ProducService(_mockHttpClient.Object);
        }

        [TestMethod]
        public async Task GetList_ShouldReturnProductsWithTax()
        {
            // Arrange
            var products = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, Title = "Iteam A", Price = 100.0 },
                new ProductDTO { Id = 2, Title = "Item B", Price = 200.0 }
            };

            _mockHttpClient.Setup(c => c.GetAsync<IEnumerable<ProductDTO>>("products"))
                           .ReturnsAsync(products);

            // Act
            var result = await _productService.GetList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(19.0, result.ElementAt(0).Tax);  // 100 * 0.19
            Assert.AreEqual(38.0, result.ElementAt(1).Tax);  // 200 * 0.19
        }

        [TestMethod]
        public async Task GetByID_ShouldReturnProductWithTax()
        {
            // Arrange
            var product = new ProductDTO { Id = 1, Title = "Item A", Price = 100.0 };
            _mockHttpClient.Setup(c => c.GetAsync<ProductDTO>("products/1"))
                           .ReturnsAsync(product);

            // Act
            var result = await _productService.GeyByID(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(19.0, result.Tax);  // 100 * 0.19
        }

        [TestMethod]
        public async Task GetByID_ShouldReturnDefaultProduct_WhenNotFound()
        {
            // Arrange
            _ = _mockHttpClient.Setup(c => c.GetAsync<ProductDTO>("products/999"))
                           .ReturnsAsync(new ProductDTO());

            // Act
            var result = await _productService.GeyByID(999);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Id);  // Producto por defecto
            Assert.AreEqual(0.0, result.Tax);
        }

        [TestMethod]
        public async Task GetList_ShouldReturnEmptyList_WhenNoProducts()
        {
            // Arrange
            _mockHttpClient.Setup(c => c.GetAsync<IEnumerable<ProductDTO>>("products"))
                           .ReturnsAsync(new List<ProductDTO>());

            // Act
            var result = await _productService.GetList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
    }
}
