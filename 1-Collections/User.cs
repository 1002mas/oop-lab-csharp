using System;
using System.Collections.Generic;

namespace Collections
{
    public class User : IUser
    {
        public User(string fullName, string username, uint? age)
        {
            Age = age;
            FullName = fullName;
            Username = username ?? throw new ArgumentException();
        }

        public uint? Age { get; }

        public string FullName { get; }

        public string Username { get; }

        public bool IsAgeDefined => Age != null;

        public bool Equals(User other)
        {
            return Username == other.Username;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return (Username != null ? Username.GetHashCode() : 0);
        }
    }
}
