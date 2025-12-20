using System.Net;
using System.Net.Http.Json;
using Products.Models;
using Products.Models.Entities;
using Xunit;

public class ProductControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_products_returns_OK()
    {
        var response = await _client.GetAsync("/api/Product");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task POST_product_creates_product()
    {
        var dto = new ProductDto
        {
            Name = "Laptop",
            Description = "Gaming Laptop",
            Price = 2500
        };

        var response = await _client.PostAsJsonAsync("/api/Product", dto);

        response.EnsureSuccessStatusCode();

        var product = await response.Content.ReadFromJsonAsync<Product>();

        Assert.NotNull(product);
        Assert.Equal("Laptop", product.Name);
        Assert.NotEqual(Guid.Empty, product.Id);
    }

    [Fact]
    public async Task GET_product_by_id_returns_product()
    {
        var dto = new ProductDto
        {
            Name = "Phone",
            Description = "Smart phone",
            Price = 800
        };

        var post = await _client.PostAsJsonAsync("/api/Product", dto);
        var created = await post.Content.ReadFromJsonAsync<Product>();

        var get = await _client.GetAsync($"/api/Product/{created!.Id}");

        get.EnsureSuccessStatusCode();

        var product = await get.Content.ReadFromJsonAsync<Product>();
        Assert.Equal("Phone", product!.Name);
    }

    [Fact]
    public async Task PUT_product_updates_data()
    {
        var dto = new ProductDto
        {
            Name = "Tablet",
            Description = "Old",
            Price = 500
        };

        var post = await _client.PostAsJsonAsync("/api/Product", dto);
        var product = await post.Content.ReadFromJsonAsync<Product>();

        var updateDto = new ProductDto
        {
            Name = "Tablet Pro",
            Description = "New",
            Price = 900
        };

        var put = await _client.PutAsJsonAsync(
            $"/api/Product/{product!.Id}", updateDto);

        put.EnsureSuccessStatusCode();

        var updated = await put.Content.ReadFromJsonAsync<Product>();
        Assert.Equal("Tablet Pro", updated!.Name);
    }

    [Fact]
    public async Task GET_invalid_id_returns_404()
    {
        var response = await _client.GetAsync($"/api/Product/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

   [Fact]
    public async Task Delete_product_removes_data()
    {
        // Arrange – create product
        var dto = new ProductDto
        {
            Name = "Tablet Pro",
            Description = "New",
            Price = 900
        };

        var postResponse = await _client.PostAsJsonAsync("/api/Product", dto);
        postResponse.EnsureSuccessStatusCode();

        var createdProduct =
            await postResponse.Content.ReadFromJsonAsync<Product>();

        // Act – delete product
        var deleteResponse =
            await _client.DeleteAsync($"/api/Product/{createdProduct!.Id}");

        // Assert – delete succeeded
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Assert – product no longer exists
        var getResponse =
            await _client.GetAsync($"/api/Product/{createdProduct.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

}
