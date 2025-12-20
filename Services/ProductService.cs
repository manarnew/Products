using Products.Models;
using Products.Models.Entities;
using Products.Repositories;
using System;
using System.Collections.Generic;

namespace Products.Services
{
    public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<Product> GetAll()
        => _repository.GetAll();

    public Product? GetById(Guid id)
        => _repository.GetById(id);

    public Product Create(ProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price
        };

        _repository.Add(product);
        _repository.Save();

        return product;
    }

    public Product? Update(Guid id, ProductDto dto)
    {
        var product = _repository.GetById(id);
        if (product == null) return null;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;

        _repository.Update(product);
        _repository.Save();

        return product;
    }

    public bool Delete(Guid id)
    {
        var product = _repository.GetById(id);
        if (product == null) return false;

        _repository.Delete(product);
        _repository.Save();

        return true;
    }
}
}