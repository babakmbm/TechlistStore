using System.Linq;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void QuantityReducer(Product product, int Quantity);

        bool SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}
