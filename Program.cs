using System;

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
            List<Product> productList = new List<Product>();

            while (true)
            {
                int choice = Menu("\nMenu:\n1. Add product\n2. Show products\n3. Exit\n");

                if (choice == 1)
                {
                    string name = AskForName("Enter product name: ");
                    Product product = new Product(name);
                    productList.Add(product);
                    Console.WriteLine($"{product.Name} was added successfully.");
                }
                else if (choice == 2)
                {
                    if (productList.Count == 0)
                    {
                        Console.WriteLine("No data available.");
                    }
                    else
                    {
                        foreach (Product p in productList)
                        {
                            Console.WriteLine($"Product: {p.Name} | Product ID: {p.Id}");
                        }
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid value.");
                }
            }
        }
        static int Menu(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 3)
                {
                    return option;
                }
                Console.WriteLine("Invalid value.");
            }
        }
        static string AskForName(string message)
        {
            while (true)
            {
                Console.Write(message);
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