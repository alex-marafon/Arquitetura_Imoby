﻿using NUnit.Framework;
using Custom_Architecture.Entities;
using Custom_Architecture.Repositories;
using System.Data;

namespace Custom_Architecture.IntegratedTests.TestCases.Repositories;

[TestFixture]
public class ProductRepositoryTest : BaseTest
{
    private readonly ProductRepository _productRepository;

    public ProductRepositoryTest()
    {
        _productRepository = new ProductRepository(TestHost<Startup>.GetService<IDbConnection>());
    }

    [Test]
    public void GetAllAsync_Check()
    {
        // assert
        Assert.DoesNotThrowAsync(() => _productRepository.GetAllAsync());
    }

    [Test]
    public void GetByIdAsync_Check()
    {
        // assert
        Assert.DoesNotThrowAsync(() => _productRepository.GetByIdAsync(1));
    }

    [Test]
    public void CreateAsync_Check()
    {
        // prepare
        var product = new Product() { Name = "test", Value = 10 };

        // assert
        Assert.DoesNotThrowAsync(() => _productRepository.CreateAsync(product));
    }

    [Test]
    public void UpdateAsync_Check()
    {
        // prepare
        var product = new Product() { Name = "test", Value = 10 };

        // assert
        Assert.DoesNotThrowAsync(() => _productRepository.UpdateAsync(product));
    }
}