using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DAL;

namespace TwitterClone.BAL
{
    public class TwitterBAL
    {
        private TwitterCloneDataContext db = new TwitterCloneDataContext();
        public PERSON GetTweetInfo(string UserID)
        {
           
            PERSON personInfo = db.PERSON.Find(UserID);
            var FollowingList = db.FOLLOWER.Where(n => n.User_ID == UserID).Select(n => n.Following_ID).ToList();
            var FollowingPersonList = (from person in db.PERSON
                                       where FollowingList.Contains(person.User_ID)
                                       select person).ToList();
            var FollowerList = db.FOLLOWER.Where(n => n.Following_ID == UserID).Select(n => n.Following_ID).ToList();
            var FollowerPersonList = (from person in db.PERSON
                                       where FollowerList.Contains(person.User_ID)
                                       select person).ToList();
            personInfo.Following = FollowingPersonList;
            personInfo.Followers = FollowerPersonList;
            List<TWEET> TweetList = (from tt in db.TWEET
                                     where FollowingList.Contains(tt.User_ID) || tt.User_ID == UserID
                                     select tt).ToList();
            personInfo.TWEETs = TweetList;
            //var tweets = personInfo.TWEETs;
            //List<FOLLOWING> fFollowing = personInfo.Following.ToList();
            //var f = db.FOLLOWER.Where(t => t.User_ID == UserID).Select(n => n.Following_ID).ToList();
            //List<TWEET> tw = (from tt in db.TWEET
            //                  where f.Contains(tt.User_ID) || tt.User_ID == UserID
            //                  select tt).ToList();


            return personInfo;


            //using (var context = new TwitterCloneDataContext())
            //{
            //    personInfo = context.PERSON.Find(UserID);
            //    return personInfo;
            //}
        }

        public bool IsAlreadyFollowing(string folllower_ID, string name)
        {
            int count=db.FOLLOWER.Where(n => n.Following_ID == folllower_ID && n.User_ID == name).ToList().Count();
            if (count > 0)
                return true;
            else
                return false;

        }

        public TWEET GetTweetDetails(int id)
        {
            return db.TWEET.Find(id);
        }

        public static void CreateUser(PERSON pERSON)
        {
            using (var context = new TwitterCloneDataContext())
            {
                context.PERSON.Add(pERSON);
                context.SaveChanges();
            }

        }

        public List<User> Search(string searchString)
        {
            var user=from N in db.PERSON
            where N.User_ID.StartsWith(searchString)
            select new User {UserID= N.User_ID };
            return user.ToList();
        }

        public void AddFollower(string userName, string name)
        {
            FOLLOWING f = new FOLLOWING();
            f.User_ID = name;
            f.Following_ID = userName;
            db.FOLLOWER.Add(f);
            db.SaveChanges();
        }

        public void AddTweet(TWEET tweet)
        {
            db.TWEET.Add(tweet);
            db.SaveChanges();
        }

        public void UpdateTweet(TWEET tweet)
        {
            db.TWEET.Find(tweet.Tweet_ID).Message = tweet.Message;
            db.SaveChanges();
        }
        public void DeleteTweet(int id)
        {
            TWEET tweet = db.TWEET.Find(id);
            db.TWEET.Remove(tweet);
            db.SaveChanges();
        }
    }
}
