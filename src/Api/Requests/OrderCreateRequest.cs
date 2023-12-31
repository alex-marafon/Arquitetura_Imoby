﻿using Custom_Architecture.Entities;

namespace Custom_Architecture.Requests;

public class OrderCreateRequest
{
    public int ClientId { get; set; }
    public OrderProductRequest[] Products { get; set; }

    public static explicit operator Order(OrderCreateRequest orderCreateRequest)
    {
        return new()
        {
            ClientId = orderCreateRequest.ClientId,
            OrderProducts = orderCreateRequest.Products.Select(x => (OrderProduct)x).ToList()
        };
    }
}
