using Remoting.Models;
using System.Collections.Generic;

namespace Remoting
{
    public interface IProductCatalog
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(string id);
        Product AddOrUpdateProduct(Product product);
        bool RemoveProduct(string id);
    }
}
