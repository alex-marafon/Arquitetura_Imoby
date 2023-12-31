﻿using Moq;
using Custom_Architecture.Entities;
using Custom_Architecture.Enums;
using Custom_Architecture.Interfaces.Repositories;
using Custom_Architecture.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Custom_Architecture.Tests.Services;

public class ProductServiceTest
{
    private readonly NotificationContext _notificationContext = new NotificationContext();
    private readonly Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();

    private ProductService GetProductService()
    {
        return new ProductService(
            _notificationContext,
            _mockProductRepository.Object);
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ReturnsProduct()
    {
        // prepare
        _mockProductRepository
            .Setup(x => x.CreateAsync(It.IsAny<Product>()))
            .ReturnsAsync(new Product());

        var service = GetProductService();
        var product = new Product()
        {
            Name = "test",
            Value = 10
        };

        // act
        var data = await service.CreateAsync(product);

        // assert
        Assert.NotNull(data);
        _mockProductRepository.Verify(x => x.CreateAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WithInvalidValue_ReturnsError()
    {
        // prepare
        _mockProductRepository
            .Setup(x => x.CreateAsync(It.IsAny<Product>()))
            .ReturnsAsync(new Product());

        var product = new Product() { Name = "Product", Value = -10 };
        var service = GetProductService();

        // act
        var data = await service.CreateAsync(product);

        // assert
        Assert.Null(data);
        Assert.False(_notificationContext.IsValid);
        Assert.Contains(_notificationContext.ErrorMessages,
            x => x.ErrorCode == "PRODUCT_VALUE_INVALID" &&
                x.Message == "Product value should be greater than 0" &&
                x.ErrorType == ErrorType.Validation);
        _mockProductRepository.Verify(x => x.CreateAsync(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task GetAllAsync_WithValidData_ReturnsProduct()
    {
        // prepare
        _mockProductRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Product>());

        var service = GetProductService();

        // act
        var data = await service.GetAllAsync();

        // assert
        Assert.NotNull(data);
    }

    [Fact]
    public async Task GetByIdAsync_WithProductThatDoesNotExist_ReturnsNull()
    {
        // prepare
        _mockProductRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(default(Product));

        var service = GetProductService();

        // act
        var data = await service.GetByIdAsync(1);

        // assert
        Assert.Null(data);
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        Assert.False(_notificationContext.IsValid);
        Assert.Contains(_notificationContext.ErrorMessages,
            x => x.ErrorCode == "PRODUCT_NOT_FOUND" &&
                x.Message == $"Product 1 not found" &&
                x.ErrorType == ErrorType.NotFound);
    }

    [Fact]
    public async Task GetByIdAsync_WithProductThatExists_ReturnsProduct()
    {
        // prepare
        _mockProductRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Product());

        var service = GetProductService();

        // act
        var data = await service.GetByIdAsync(1);

        // assert
        Assert.NotNull(data);
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        Assert.True(_notificationContext.IsValid);
    }

    [Fact]
    public async Task UpdateAsync_WithProductThatDoesNotExist_ReturnsNull()
    {
        // prepare
        _mockProductRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(default(Product));

        var service = GetProductService();
        var product = new Product() { ProductId = 1, Value = 10 };

        // act
        var data = await service.UpdateAsync(product);

        // assert
        Assert.Null(data);
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockProductRepository.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Never);
        Assert.False(_notificationContext.IsValid);
        Assert.Contains(_notificationContext.ErrorMessages,
            x => x.ErrorCode == "PRODUCT_NOT_FOUND" &&
                x.Message == $"Product 1 not found" &&
                x.ErrorType == ErrorType.Validation);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidValueForProduct_ReturnsNull()
    {
        // prepare
        _mockProductRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(default(Product));

        var service = GetProductService();
        var product = new Product()
        {
            ProductId = 1,
            Value = 0
        };

        // act
        var data = await service.UpdateAsync(product);

        // assert
        Assert.Null(data);
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
        _mockProductRepository.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Never);
        Assert.False(_notificationContext.IsValid);
        Assert.Contains(_notificationContext.ErrorMessages,
            x => x.ErrorCode == "PRODUCT_VALUE_INVALID" &&
                x.Message == $"Product value should be greater than 0" &&
                x.ErrorType == ErrorType.Validation);
    }

    [Fact]
    public async Task UpdateAsync_WithProductThatExists_ReturnsProduct()
    {
        // prepare
        _mockProductRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Product());

        _mockProductRepository
            .Setup(x => x.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync(new Product());

        var service = GetProductService();
        var product = new Product()
        {
            ProductId = 10,
            Name = "Product",
            Value = 15
        };

        // act
        var data = await service.UpdateAsync(product);

        // assert
        Assert.NotNull(data);
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockProductRepository.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Once);
        Assert.True(_notificationContext.IsValid);
    }
}
