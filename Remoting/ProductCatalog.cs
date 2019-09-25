using System;
using System.Collections.Generic;
using Remoting.Models;

namespace Remoting
{
    public class ProductCatalog : MarshalByRefObject, IProductCatalog
    {
        private readonly Dictionary<string, Product> products = new Dictionary<string, Product> {
            { "A1", new Product { Description = "Sample product 1", Id = "A1", Name = "Widget", Price = 9.99 } },
            { "B2", new Product { Description = "Sample product 2", Id = "B2", Name = "Mini-widget", Price = 4.99 } },
            { "C3", new Product { Description = "Sample product 3", Id = "C3", Name = "Widget XL", Price = 14.99 } }
        };

        public Product AddOrUpdateProduct(Product product)
        {
            EnsureIdSet(product);
            products[product.Id] = product;
            return product;
        }

        public Product GetProduct(string id) => products.ContainsKey(id) ? products[id] : null;

        public IEnumerable<Product> GetProducts() => products.Values;

        public bool RemoveProduct(string id) => products.Remove(id);

        private void EnsureIdSet(Product product)
        {
            if (string.IsNullOrEmpty(product.Id))
            {
                product.Id = Guid.NewGuid().ToString();
            }
        }
    }
}
