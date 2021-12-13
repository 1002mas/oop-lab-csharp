using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private Dictionary<string, HashSet<TUser>> _friends = new Dictionary<string, HashSet<TUser>>();
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            if (!_friends.ContainsKey(group))
            {
                _friends.Add(group, new HashSet<TUser>());
            }
            if(_friends[group].Contains(user))
            {
                return false;
            }
            _friends[group].Add(user);
            return true;
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                HashSet<TUser> friends = new HashSet<TUser>();
                foreach (var group in _friends)
                {
                    foreach (var friend in group.Value)
                    {
                        friends.Add(friend);
                    }
                }
                return new List<TUser>(friends);
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            if (_friends.ContainsKey(group))
            {
                return new List<TUser>(_friends[group]);
            }

            return new List<TUser>();
        }
    }
}
