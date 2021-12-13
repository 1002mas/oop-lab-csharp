using System;

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

        // TODO implement missing methods (try to autonomously figure out which are the necessary methods)
    }
}
