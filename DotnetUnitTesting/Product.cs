using System;

namespace DotnetUnitTesting
{
    public class Product
    {
        public Product(int id, string name, double price, double quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get; }
        public string Name { get; }
        public double Price { get; }
        public double Quantity { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}. Name: {Name}. Price: {Price}. Quantity: {Quantity}";
        }
    }
}