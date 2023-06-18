using Custom_Architecture.Entities;

namespace Custom_Architecture.Requests;

public class ClientCreateRequest
{
    public string Name { get; set; }

    public static explicit operator Client(ClientCreateRequest clientCreateRequest)
    {
        return new()
        {
            Name = clientCreateRequest.Name
        };
    }
}
