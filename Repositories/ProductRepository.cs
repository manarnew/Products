using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Data;
using Products.Models.Entities;
namespace Products.Repositories
{
    public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Product> GetAll()
        => _dbContext.Products.ToList();

    public Product? GetById(Guid id)
        => _dbContext.Products.Find(id);

    public void Add(Product product)
        => _dbContext.Products.Add(product);

    public void Update(Product product)
        => _dbContext.Products.Update(product);

    public void Delete(Product product)
        => _dbContext.Products.Remove(product);

    public void Save()
        => _dbContext.SaveChanges();
}
}