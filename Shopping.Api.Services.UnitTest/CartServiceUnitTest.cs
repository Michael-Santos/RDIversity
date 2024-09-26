using Moq;
using NUnit.Framework;
using Shopping.Api.Repositories;
using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Services.UnitTest
{
    [TestFixture]
    public class CartServiceUnitTest
    {
        private MockRepository _mockRepository;
        private Mock<ICartRepository> _cartRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private CartService _cartService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _cartRepositoryMock = _mockRepository.Create<ICartRepository>();
            _productRepositoryMock = _mockRepository.Create<IProductRepository>();
            _cartService = new CartService(_cartRepositoryMock.Object, _productRepositoryMock.Object);
        }

        [Test]
        public void CreateCart_WhenProductIsNotFound_ShouldThrowException()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetById(1)).Returns((Product)null).Verifiable();

            // Act
            TestDelegate act = () => _cartService.CreateCart(1, 3);

            // Assert
            var exception = Assert.Throws<ArgumentException>(act);
            Assert.That("Product was not found.", Is.EqualTo(exception.Message));
            _mockRepository.Verify();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CreateCart_WhenQuantityIsEqualOrLessThenZero_ShouldThrowException(int quantity)
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test", Price = 1 };
            _productRepositoryMock.Setup(x => x.GetById(1)).Returns(product).Verifiable();

            // Act
            TestDelegate act = () => _cartService.CreateCart(1, quantity);

            // Assert
            var exception = Assert.Throws<ArgumentException>(act);
            Assert.That("Quantity must be greater than 0.", Is.EqualTo(exception.Message));
            _mockRepository.Verify();
        }

        [Test]
        public void CreateCart_WhenProductExistsAndQuantityIsValid_ShouldSaveCart()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test", Price = 1 };
            _productRepositoryMock.Setup(x => x.GetById(1)).Returns(product).Verifiable();
            _cartRepositoryMock.Setup(x => x.Add(It.IsAny<Cart>())).Verifiable();
            _cartRepositoryMock.Setup(x => x.Save()).Verifiable();

            // Act
            var result = _cartService.CreateCart(1, 3);

            // Assert
            Assert.That(result.Items.First().ProductId, Is.EqualTo(product.Id));
            _mockRepository.Verify();
        }
    }
}