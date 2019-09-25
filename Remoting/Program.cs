using System;
using System.Linq;

namespace Remoting
{
    class Program
    {
        static void Main()
        {
            var catalog =
#if NET472
                LoggingProxy<ProductCatalog>.Decorate(new ProductCatalog());
#elif NETCOREAPP
                LoggingProxy<IProductCatalog>.Decorate(new ProductCatalog());
#endif

            // Get products
            var products = catalog.GetProducts();

            Console.WriteLine($"\nRetrieved {products.Count()} products\n");

            // Change the first one
            var firstProduct = products.First();
            firstProduct.Price++;
            catalog.AddOrUpdateProduct(firstProduct);

            Console.WriteLine($"\nIncreased price for product {firstProduct.Name}\n");

            Console.WriteLine("Done");
        }
    }
}
