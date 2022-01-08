using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class ProductsDbStorage : IProductsStorage
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

       public List<Product> GetAll { get { return databaseContext.Products.ToList(); } }

        /// <summary> Finds Product with exact id </summary>
        public Product TryFindProductById(Guid productId)
        {
            var curentProduct = databaseContext.Products.FirstOrDefault(x => x.Id == productId);

            return curentProduct;
        }

        /// <summary>adds new product</summary>
        /// <param name="product">product to add</param>
        public void Add(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        /// <summary> Totaly removes product</summary>
        public void Remove(Guid productId)
        {
            databaseContext.Products.Remove(TryFindProductById(productId));
            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Replaces value of field if it was changed(not null) 
        /// It can be Name/Cost/Description/ImagePath.
        /// </summary>
        /// <param name="product">Info of product with changes</param>
        public void EditProductInfo(Product product)
        {
            var productToChange = TryFindProductById(product.Id);

            productToChange.Name = product?.Name ?? productToChange.Name;
            productToChange.Cost = product?.Cost ?? productToChange.Cost;
            productToChange.Description = product?.Description ?? productToChange.Description;
            productToChange.ImagePath = product?.ImagePath ?? productToChange.ImagePath;
            productToChange.Category = product?.Category ?? productToChange.Category;

            databaseContext.SaveChanges();
        }
    }
}
