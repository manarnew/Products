using Products.Models.Entities;
using System;
using System.Collections.Generic;

namespace Products.Repositories
{
    public interface IProductRepository
    {
            List<Product> GetAll();
            Product? GetById(Guid id);
            void Add(Product product);
            void Update(Product product);
            void Delete(Product product);
            void Save();
    }
}