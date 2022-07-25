namespace DotnetUnitTesting
{
    public class UserAccount
    {
        public UserAccount(string name, string surname, string dateOfBirth)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            ShoppingCart = new ShoppingCart();
        }

        public string Name { get; }
        public string Surname { get; }
        public string DateOfBirth { get; }
        public ShoppingCart ShoppingCart { get; }
    }
}