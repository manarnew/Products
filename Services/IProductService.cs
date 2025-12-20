using Products.Models;
using Products.Models.Entities;
using System;
using System.Collections.Generic;
namespace Products.Services
{
   public interface IProductService
    {
        List<Product> GetAll();
        Product? GetById(Guid id);
        Product Create(ProductDto dto);
        Product? Update(Guid id, ProductDto dto);
        bool Delete(Guid id);
    }
}