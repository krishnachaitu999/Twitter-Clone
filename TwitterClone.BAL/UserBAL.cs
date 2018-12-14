using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DAL;

namespace TwitterClone.BAL
{
    public class UserBAL
    {
        private TwitterCloneDataContext db = new TwitterCloneDataContext();
        public bool ValidateUser(PERSON LoginInfo)
        {
            using (var context = new TwitterCloneDataContext())
            {
                var query = from st in context.PERSON
                            where st.User_ID == LoginInfo.User_ID && st.Password == LoginInfo.Password
                            select st;
                if (query.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static void CreateUser(PERSON pERSON)
        {
            using (var context = new TwitterCloneDataContext())
            {
                context.PERSON.Add(pERSON);
                context.SaveChanges();
            }
        
        }

        public bool IsUserExist(string user_ID)
        {
            PERSON p = db.PERSON.Find(user_ID);
            if (p != null)
                return true;
            else
                return false;
        }

        public PERSON GetUserDetails(string id)
        {
            return db.PERSON.Find(id);
        }
    }
}
