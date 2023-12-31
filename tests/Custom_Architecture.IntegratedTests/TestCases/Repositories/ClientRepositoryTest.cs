﻿using NUnit.Framework;
using Custom_Architecture.Entities;
using Custom_Architecture.Repositories;
using System.Data;

namespace Custom_Architecture.IntegratedTests.TestCases.Repositories;

[TestFixture]
public class ClientRepositoryTest : BaseTest
{
    private readonly ClientRepository _clientRepository;

    public ClientRepositoryTest()
    {
        _clientRepository = new ClientRepository(TestHost<Startup>.GetService<IDbConnection>());
    }

    [Test]
    public void GetAllAsync_Check()
    {
        // assert
        Assert.DoesNotThrowAsync(() => _clientRepository.GetAllAsync());
    }

    [Test]
    public void GetByIdAsync_Check()
    {
        // assert
        Assert.DoesNotThrowAsync(() => _clientRepository.GetByIdAsync(1));
    }

    [Test]
    public void CreateAsync_Check()
    {
        // prepare
        var client = new Client() { Name = "test" };

        // assert
        Assert.DoesNotThrowAsync(() => _clientRepository.CreateAsync(client));
    }

    [Test]
    public void UpdateAsync_Check()
    {
        // prepare
        var client = new Client() { Name = "test" };

        // assert
        Assert.DoesNotThrowAsync(() => _clientRepository.UpdateAsync(client));
    }
}
