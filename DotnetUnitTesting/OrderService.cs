namespace DotnetUnitTesting
{
    public class OrderService
    {
        private readonly IDiscountUtility _discountUtility;

        public OrderService(IDiscountUtility discountUtility)
        {
            _discountUtility = discountUtility;
        }

        public double GetOrderPrice(UserAccount user)
        {
            var discount = _discountUtility.CalculateDiscount(user);
            return user.ShoppingCart.GetCartTotalPrice() - discount;
        }
    }
}