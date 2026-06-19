using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace ReadOnlyIdGeneratorSys
{
    public class Product
    {
        private static int counter = 1;
        public int Id { get; }
        public string Name { get; private set; }

        public Product(string name)
        {
            Name = name.Trim();
            Id = counter;
            counter++;
        }

    }

    public class Program
    {
        static void Main(string[] arg)
        {
            int amount = AskForAmount("Enter number of products: ");
            for(int i = 1; i <= amount; i++)
            {
                string name = AskForName("Enter products name: ");
                Product product = new Product(name);
                Console.WriteLine($"Product: {product.Name} | Product ID: {product.Id}");
            }
        }
        static int AskForAmount(string message)
        {
            while (true)
            {
                Console.Write(message);
                if(int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
                {
                    return amount;
                }
                else
                {
                    Console.WriteLine("Invalid value.");
                }
            }
        }
        static string AskForName(string message)
        {
            while (true)
            {
                System.Console.Write(message);
                string? name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    return name.Trim();
                }
                else
                {
                    Console.WriteLine("Invalid value.");
                }
            }
        }
    }
}