﻿using Custom_Architecture.Entities;

namespace Custom_Architecture.Responses;

public class ClientCreateResponse
{
    public int ClientId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }

    public static explicit operator ClientCreateResponse(Client client)
    {
        return new()
        {
            ClientId = client.ClientId,
            Name = client.Name,
            CreatedDate = client.CreateDate
        };
    }
}
