namespace Webshop.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }

        void Dispose();
        Task<int> SaveAsync();
    }
}