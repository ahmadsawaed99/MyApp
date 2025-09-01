using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Moq;
using NPOI.HPSF;

namespace Backend.Tests;

public class ProductRepositoryTests
{
    private readonly ProductRepository _sut;
    private readonly Mock<IMongoDBService<Product>> _mongoServiceMock = new Mock<IMongoDBService<Product>>();
    public ProductRepositoryTests()
    {
        _sut = new ProductRepository(_mongoServiceMock.Object);
    }

    [Fact]
    public async Task Test1()
    {
        // Arrange
        var id = Guid.NewGuid().ToString("N");
        var product = new Product()
        {
            Id = id,
        };
        _mongoServiceMock.Setup(m => m.AddToDatabase(product, "products"))
        .ReturnsAsync(id);
        // Act
        var productId = await _sut.AddPoductToDatabase(product);
        // Assert

        Assert.Equal(product.Id, "sad");
    }
}