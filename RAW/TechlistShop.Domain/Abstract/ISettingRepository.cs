using System.Linq;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Abstract
{
    public interface ISettingRepository
    {
        IQueryable<Setting> Settings { get; }
        void SaveSetting(Setting Setting);
    }
}
