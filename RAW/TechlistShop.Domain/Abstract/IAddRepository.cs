using System.Linq;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Abstract
{
    public interface IAddRepository
    {
        IQueryable<Add> Adds { get; }
        bool SaveAdd(Add Add);
        Add DeleteAdd(int addId);
    }
}
