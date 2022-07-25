using System.Collections.Generic;
using System.Linq;

namespace DotnetUnitTesting
{
    public class ShoppingCart
    {
        public List<Product> Products { get; }

        public ShoppingCart()
        {
            Products = new List<Product>();
        }

        public ShoppingCart(List<Product> products)
        {
            Products = products;
        }

        public void AddProductToCart(Product productToAdd)
        {
            var productToAddId = productToAdd.Id;
            if (GetProductIds().Contains(productToAddId))
            {
                var existingProduct = GetProductById(productToAddId);
                var initialProductQuantity = existingProduct.Quantity;
                existingProduct.Quantity = initialProductQuantity + productToAdd.Quantity;
            }
            else
            {
                Products.Add(productToAdd);
                //Products.Add(new Product(productToAddId, productToAdd.Name, productToAdd.Price, productToAdd.Quantity));
            }
        }

        public void RemoveProductFromCart(Product product)
        {
            if (GetProductIds().Contains(product.Id))
            {
                Products.Remove(product);
            }
            else
            {
                throw new ProductNotFoundException($"Product is not found in the cart: {product}");
            }
        }

        public double GetCartTotalPrice()
        {
            return Products.Sum(product => product.Quantity * product.Price);
        }

        private List<int> GetProductIds()
        {
            return Products.Select(productInCart => productInCart.Id).ToList();
        }

        public Product GetProductById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return product;
            }

            throw new ProductNotFoundException($"Product with {id} ID is not found in the shopping cart!");
        }

    }
}