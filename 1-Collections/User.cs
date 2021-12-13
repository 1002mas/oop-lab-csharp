using System;
using System.Collections.Generic;

namespace Collections
{
    public class User : IUser
    {
        private uint? _age;
        private string _fullName;
        private string _username;
        public User(string fullName, string username, uint? age)
        {
            _age = age;
            _fullName = fullName;
            _username = username;
            if (_username == null)
            {
                throw new ArgumentException();
            }
        }

        public uint? Age { get => _age; }

        public string FullName { get => _fullName; }

        public string Username { get => _username; }

        public bool IsAgeDefined => _age != null;

        public bool Equals(User other)
        {
            return _username == other._username;
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
            return (_username != null ? _username.GetHashCode() : 0);
        }
    }
}
