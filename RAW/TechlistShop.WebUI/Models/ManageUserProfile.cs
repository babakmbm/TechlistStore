using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechlistShop.WebUI.Models
{
    public class ManageUserProfile
    {
        UsersContext context = new UsersContext();

        public bool SaveUser(UserProfile user)
        {
            bool isNew;
            if (user.UserId == 0)
            {
                context.UserProfiles.Add(user);
                isNew = true;
            }
            else
            {
                UserProfile dbEntry = context.UserProfiles.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = user.FirstName;
                    dbEntry.LastName = user.LastName;
                    dbEntry.Address = user.Address;
                    dbEntry.City = user.City;
                    dbEntry.State = user.State;
                    dbEntry.PostalCode = user.PostalCode;
                    dbEntry.Country = user.Country;
                    dbEntry.LastName = user.LandLine;
                    dbEntry.CellPhone = user.CellPhone;
                }
                isNew = false;
            }
            context.SaveChanges();
            return isNew;
        }

        public UserProfile DeleteUser(int userId)
        {
            UserProfile dbEntry = context.UserProfiles.Find(userId);

            if (dbEntry != null)
            {
                context.UserProfiles.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}