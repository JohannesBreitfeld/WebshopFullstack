namespace Webshop.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IProductCategoryRepository ProductCategories { get; }

        void Dispose();
        Task<int> SaveAsync();
    }
}