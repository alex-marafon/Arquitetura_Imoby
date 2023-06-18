using Custom_Architecture.Entities;

namespace Custom_Architecture.Requests;

public class ProductCreateRequest
{
    public string Name { get; set; }
    public decimal Value { get; set; }

    public static explicit operator Product(ProductCreateRequest productCreateRequest)
    {
        return new()
        {
            Name = productCreateRequest.Name,
            Value = productCreateRequest.Value
        };
    }
}