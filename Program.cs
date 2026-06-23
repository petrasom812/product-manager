using System;

namespace ReadOnlyIdGeneratorSys
{
    public class Product
    {
        private static int counter = 1;

        public int Id { get; }
        public string Name { get; private set; }

        public DateTime AddedDate { get; private set; }

        public Product(string name)
        {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Name = "Unknown";
            }
            else
            {
                if (name.Length == 1)
                {
                    Name = char.ToUpper(name[0]).ToString();
                }
                else
                {
                    Name = char.ToUpper(name[0]) + name[1..].ToLower();
                }
            }
            Id = counter;
            counter++;
            AddedDate = DateTime.Now;
        }
    }

    public class ProductService
    {
        private List<Product> productList = new List<Product>();

        public void AddProduct(string name)
        {
            Product p = new(name);
            productList.Add(p);
        }

        public List<Product> GetProducts()
        {
            return productList.ToList();
        }

        public Product? SearchProduct(string name)
        {
            return productList.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<(string Name, int Count)> GetProductSummary()
        {
            return productList
                .GroupBy(p => p.Name)
                .Select(g => (Name: g.Key, Count: g.Count()))
                .ToList();
        }
        public bool DeleteProduct(string name)
        {
            var product = productList
                .FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product == null)
                return false;

            productList.Remove(product);
            return true;
        }
    }

    public class Program
    {

        static void Main(string[] arg)
        {
            ProductService service = new ProductService();
            while (true)
            {
                Console.WriteLine("\nMenu:\n1. Add product\n2. Show products\n3. Search Product\n4. Delete product\n5. Summary\n6. Exit\n");
                int choice = Menu("Choose one option: ");

                if (choice == 1)
                {
                    string name = AskForName("Enter product name: ");
                    service.AddProduct(name);
                    Console.WriteLine($"{name} was added successfully.");
                }
                else if (choice == 2)
                {
                    var products = service.GetProducts();
                    if (!products.Any())
                    {
                        Console.WriteLine("No data available.");
                    }
                    else
                    {
                        DisplayProducts(products);
                    }
                    Console.WriteLine();
                }
                else if (choice == 3)
                {
                    string name = AskForName("Enter product name to search: ");
                    var product = service.SearchProduct(name);
                    if (product != null)
                    {
                        DisplayProduct(product);
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");
                    }
                }
                else if (choice == 4)
                {
                    string name = AskForName("Enter product name to delete: ");
                    bool deleted = service.DeleteProduct(name);
                    if (deleted)
                    {
                        Console.WriteLine($"{name} deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");
                    }
                }
                else if (choice == 5)
                {
                    var products = service.GetProductSummary();

                    Console.WriteLine($"\n\n--- Product Summary ---\n");
                    DisplayProductSummary(products);
                }
                else if (choice == 6)
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
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 6)
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
        static void DisplayProduct(Product product)
        {
            Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Added Date: {product.AddedDate:g}");
        }

        static void DisplayProducts(List<Product> products)
        {
            foreach (var p in products)
            {
                Console.WriteLine($"ID: {p.Id} | Name: {p.Name} | Added Date: {p.AddedDate:g}");
            }
        }
        static void DisplayProductSummary(List<(string Name, int Count)> summary)
        {

            foreach (var p in summary)
            {
                Console.WriteLine($"{p.Name}: {p.Count}");
            }

        }
    }
}