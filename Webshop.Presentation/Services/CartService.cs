using Blazored.LocalStorage;

namespace Webshop.Presentation.Services;

public class CartService
{
    private readonly ILocalStorageService _localStorage;
    private const string CartKey = "shopping_cart";
    public decimal Total => Items.Sum(item => item.Price * item.Quantity);
    public event Action OnCartChanged;

    public List<CartItem> Items { get; private set; } = new();

    public CartService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task LoadCartAsync()
    {
        Items = await _localStorage.GetItemAsync<List<CartItem>>(CartKey) ?? new();
        OnCartChanged?.Invoke();
    }

    public async Task AddToCartAsync(CartItem item)
    {
        Items.Add(item);
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
}

public class CartItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

