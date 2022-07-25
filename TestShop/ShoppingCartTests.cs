using DotnetUnitTesting;
using Xunit;

namespace TestShop
{
    public class ShoppingCartTests
    {
        private int _id;
        private double _price;
        private double _quantity;

        private UserAccount _user;
        private Product _createdProduct;

        public ShoppingCartTests()
        {
            _user = new UserAccount(Faker.Name.FirstName(), Faker.Name.LastName(), new DateTime(1999, 12, 25).ToString());

            _id = Faker.Number.RandomNumber(100000);
            _price = 1000 * Faker.Number.Double();
            _quantity = Faker.Number.RandomNumber(1000);
            _createdProduct = CreateProduct(_id, _price, _quantity);
            
            _user.ShoppingCart.AddProductToCart(_createdProduct);
        }

        [Fact]
        public void AddProduct_NonExisted()
        {

            // Assert
            VerifyProductsInList(_createdProduct);
            Assert.Single(_user.ShoppingCart.Products);
        }

        [Fact]
        public void AddProduct_Existed()
        {
            var id2 = Faker.Number.RandomNumber(100000);
            var price2 = 1000 * Faker.Number.Double();
            var quantity2 = Faker.Number.RandomNumber(1000);
            var createdProduct2 = CreateProduct(id2, price2, quantity2);

            // Act
            _user.ShoppingCart.AddProductToCart(createdProduct2);

            // Assert
            VerifyProductsInList(_createdProduct);
            VerifyProductsInList(createdProduct2);
            Assert.Equal(2, _user.ShoppingCart.Products.Count);
        }

        [Fact]
        public void Remove_Existed()
        {
            // Act
            _user.ShoppingCart.RemoveProductFromCart(_createdProduct);

            // Assert
            Assert.Throws<ProductNotFoundException>(() => _user.ShoppingCart.GetProductById(_id));
        }

        [Fact]
        public void Remove_NotExisted()
        {
            // Act
            var randomProduct = CreateProduct(Faker.Number.RandomNumber(10000), _price, _quantity);

            // Assert
            Assert.Throws<ProductNotFoundException>(() => _user.ShoppingCart.RemoveProductFromCart(randomProduct));
        }

        [Fact]
        public void GetTotalPrice()
        {
            // Arrange
            var id2 = Faker.Number.RandomNumber(100000);
            var price2 = 1000 * Faker.Number.Double();
            var quantity2 = Faker.Number.RandomNumber(1000);
            var createdProduct2 = CreateProduct(id2, price2, quantity2);

            // Act
            _user.ShoppingCart.AddProductToCart(createdProduct2);

            // Assert
            Assert.Equal(_price * _quantity + price2 * quantity2, _user.ShoppingCart.GetCartTotalPrice());

            _user.ShoppingCart.RemoveProductFromCart(createdProduct2);

            Assert.Equal(_price * _quantity, _user.ShoppingCart.GetCartTotalPrice());
        }

        [Fact]
        public void TestDiscountUsingMoq()
        {
            var discount = 3;

            var mockDiscount = new Moq.Mock<IDiscountUtility>();
            mockDiscount.Setup(obj => obj.CalculateDiscount(_user)).Returns(discount);

            var orderPrice = new OrderService(mockDiscount.Object);

            Assert.Equal(_user.ShoppingCart.GetCartTotalPrice() - discount, orderPrice.GetOrderPrice(_user));
            mockDiscount.VerifyAll();
        }

        private Product CreateProduct(int id, double price, double quantity)
        {
            return new Product(id, Faker.Name.FirstName(), price, quantity);
        }

        private void VerifyProductsInList(Product product)
        {
            var actualProduct = _user.ShoppingCart.GetProductById(product.Id);
            Assert.Equal(product.ToString(), actualProduct.ToString());
        }
    }
}