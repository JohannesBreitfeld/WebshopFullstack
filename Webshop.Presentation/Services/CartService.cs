using Blazored.LocalStorage;
using Webshop.Application.DTOs.Requests;
using Webshop.Presentation.Models;

namespace Webshop.Presentation.Services;

public class CartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;
    private const string CartKey = "shopping_cart";
    public double Total => Items.Sum(item => item.Price * item.Quantity);
    public event Action OnCartChanged;

    public List<CartItem> Items { get; private set; } = new();

    public CartService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    public async Task LoadCartAsync()
    {
        Items = await _localStorage.GetItemAsync<List<CartItem>>(CartKey) ?? new();
        OnCartChanged?.Invoke();
    }

    public async Task AddToCartAsync(ProductModel product)
    {
        var existingItem = Items.FirstOrDefault(i => i.Id == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        { 
            Items.Add(new CartItem
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1
            });
        }

        await SaveCartAsync();
    }

    public async Task RemoveFromCartAsync(CartItem item)
    {
        Items.Remove(item);
        await SaveCartAsync();
    }

    private async Task SaveCartAsync()
    {
        await _localStorage.SetItemAsync(CartKey, Items);
        OnCartChanged?.Invoke();
    }

    public async Task ClearCartAsync()
    {
        Items.Clear();
        await _localStorage.RemoveItemAsync(CartKey);
        OnCartChanged?.Invoke();
    }

    public async Task<bool> PlaceOrderAsync(int customerId, List<CartItem> products)
    {
        if (products.Count == 0)
        {
            return false;
        }

        var client = _httpClientFactory.CreateClient("API");

        var customerResponse = await client.GetAsync($"api/customers/{customerId}");

        if (!customerResponse.IsSuccessStatusCode)
        {
            return false; 
        }

        
        var order = new CreateOrderRequest
        {
            CustomerId = customerId,
            Products = products.Select(p => new CreateOrderProductRequest
            {
                ProductId = p.Id,
                Quantity = p.Quantity
            }).ToList()
        };

        var orderResponse = await client.PostAsJsonAsync("api/orders", order);

        return orderResponse.IsSuccessStatusCode;
    }
}



