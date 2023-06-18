using Custom_Architecture.Entities;

namespace Custom_Architecture.Requests;

public class ClientPatchRequest
{
    public string Name { get; set; }

    public static explicit operator Client(ClientPatchRequest clientPatchRequest)
    {
        return new()
        {
            Name = clientPatchRequest.Name
        };
    }
}

