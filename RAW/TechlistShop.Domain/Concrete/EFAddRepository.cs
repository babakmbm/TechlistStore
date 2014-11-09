using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechlistShop.Domain.Abstract;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Concrete
{
    public class EFAddRepository : IAddRepository
    {
        EFDbContext context = new EFDbContext();

        public IQueryable<Add> Adds
        {
            get { return context.Adds; }
        }

        public bool SaveAdd(Add Add)
        {
            bool isNew;
            if (Add.AddId == 0)
            {
                context.Adds.Add(Add);
                isNew = true;
            }
            else
            {
                Add dbEntry = context.Adds.Find(Add.AddId);
                if (dbEntry != null)
                {
                    dbEntry.Name = Add.Name;
                    dbEntry.LinkUrl = Add.LinkUrl;
                    dbEntry.Description = Add.Description;
                    dbEntry.Active = Add.Active;
                    dbEntry.Order = Add.Order;
                    if (Add.ImageData != null)
                    {
                        dbEntry.ImageData = Add.ImageData;
                        dbEntry.ImageMimeType = Add.ImageMimeType;
                    }
                }
                isNew = false;
            }
            context.SaveChanges();
            return isNew;
        }

        public Add DeleteAdd(int addId)
        {
            Add dbEntry = context.Adds.Find(addId);

            if (dbEntry != null)
            {
                context.Adds.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
