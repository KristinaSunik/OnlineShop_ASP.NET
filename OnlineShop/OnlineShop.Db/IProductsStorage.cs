using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface IProductsStorage
    {
        /// <summary>Gets the entire list of Products from Db </summary>
        List<Product> GetAll { get; }

        /// <summary> Finds Product with exact id </summary>
        Product TryFindProductById(Guid productId);

        /// <summary> Replaces value of field if it was changed(not null) 
        /// It can be Name/Cost/Description/ImagePath.</summary>
        /// <param name="product">Info of product with changes</param>
        void EditProductInfo(Product product);

        /// <summary> Totaly removes product</summary>
        void Remove(Guid productId);

        /// <summary>adds new product</summary>
        /// <param name="product">product to add</param>
        void Add(Product product);
    }
}