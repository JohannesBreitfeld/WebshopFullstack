﻿namespace Webshop.Application.DTOs.Responses
{
    public class ProductsResponse
    {
        public required IEnumerable<ProductResponse> Items { get; init; } = Enumerable.Empty<ProductResponse>();
    }
}
