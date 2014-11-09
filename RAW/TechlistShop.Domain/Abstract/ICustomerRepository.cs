using System.Linq;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Abstract
{
    interface ICustomerRepository
    {
        IQueryable<Customer> Customers { get; }
        void SaveCustomer(Customer Customer);
        Customer DeleteCustomer(int CustomerID);
    }
}
