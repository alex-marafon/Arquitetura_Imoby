﻿using Dapper;
using Custom_Architecture.Entities;
using Custom_Architecture.Interfaces.Repositories;
using System.Data;

namespace Custom_Architecture.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnection _dbConnection;

    public OrderRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var orderDictionary = new Dictionary<int, Order>();

        var data = await _dbConnection.QueryAsync<Order, Client, OrderProduct, Product, Order>(
            @"SELECT 
	            O.OrderId,
	            O.CreateDate,
	            C.ClientId,
	            C.Name,
                OP.OrderProductId,
                OP.ProductId,
	            OP.Quantity,            
                P.ProductId,
	            P.Name,
	            P.Value	            
            FROM [dbo].[Order] O
            INNER JOIN [dbo].[Client] C ON C.ClientId = O.ClientId
            INNER JOIN [dbo].[OrderProduct] OP ON OP.OrderId = O.OrderId
            INNER JOIN [dbo].[Product] P ON P.ProductId = OP.ProductId",
            (order, client, orderProduct, product) =>
            {
                Order orderEntry;

                if (!orderDictionary.TryGetValue(order.OrderId, out orderEntry))
                {
                    orderEntry = order;
                    orderEntry.OrderProducts = new List<OrderProduct>();
                    orderDictionary.Add(orderEntry.OrderId, orderEntry);
                }

                orderEntry.Client = client;

                orderProduct.Product = product;

                orderEntry.OrderProducts.Add(orderProduct);

                return orderEntry;
            }, splitOn: "ClientId,OrderProductId, ProductId");

        return data
            .Distinct()
            .ToList();
    }

    public async Task<Order> GetByIdAsync(int orderId)
    {
        var orderDictionary = new Dictionary<int, Order>();

        var data = await _dbConnection.QueryAsync<Order, Client, OrderProduct, Product, Order>(
             @"SELECT 
	            O.OrderId,
	            O.CreateDate,
	            C.ClientId,
	            C.Name,
                OP.OrderProductId,
                OP.ProductId,
	            OP.Quantity,            
                P.ProductId,
	            P.Name,
	            P.Value	            
            FROM [dbo].[Order] O
            INNER JOIN [dbo].[Client] C ON C.ClientId = O.ClientId
            INNER JOIN [dbo].[OrderProduct] OP ON OP.OrderId = O.OrderId
            INNER JOIN [dbo].[Product] P ON P.ProductId = OP.ProductId
            WHERE
                O.OrderId = @OrderId",
             (order, client, orderProduct, product) =>
             {
                 Order orderEntry;

                 if (!orderDictionary.TryGetValue(order.OrderId, out orderEntry))
                 {
                     orderEntry = order;
                     orderEntry.OrderProducts = new List<OrderProduct>();
                     orderDictionary.Add(orderEntry.OrderId, orderEntry);
                 }

                 orderEntry.Client = client;

                 orderProduct.Product = product;

                 orderEntry.OrderProducts.Add(orderProduct);

                 return orderEntry;
             }, splitOn: "ClientId,OrderProductId, ProductId", param: new { OrderId = orderId });

        return data
            .Distinct()
            .FirstOrDefault();
    }

    public async Task<Order> CreateAsync(Order order)
    {
        var data = await _dbConnection.QueryFirstAsync<Order>(
           @"INSERT INTO [dbo].[Order](ClientId)
                VALUES(@ClientId);
            
            SELECT
                OrderId,
                ClientId,
                CreateDate
            FROM [dbo].[Order]
            WHERE 
                OrderId = SCOPE_IDENTITY()",
           new { order.ClientId });

        return data;
    }
}
