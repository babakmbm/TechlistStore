using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechlistShop.Domain.Abstract;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Concrete
{
    public class EFSettingRepository : ISettingRepository
    {
        EFDbContext context = new EFDbContext();

        public IQueryable<Setting> Settings
        {
            get { return context.Settings; }
        }

        public void SaveSetting(Setting Setting)
        {
            Setting dbEntry = context.Settings.Find(Setting.SettingID);
            if (dbEntry != null)
            {
                dbEntry.MCNOPIC = Setting.MCNOPIC;
                dbEntry.DISNOPIC = Setting.DISNOPIC;
                dbEntry.NOA = Setting.NOA;
                dbEntry.LPS = Setting.LPS;
                dbEntry.DateModified = Setting.DateModified;
                dbEntry.Description = Setting.Description;
            }
            context.SaveChanges();
        }

    }
}
