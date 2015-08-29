using System;
using System.Collections.Generic;
using System.Linq;

using Common;

namespace AopDemo.Domain.User
{
    public class DummyUserRepository : IUserRepository
    {
        private static readonly List<User> users = new List<User>
        {
            new User(1, "abc", null), 
            new User(2, "secret", null)
        };

        public User Get(int id)
        {
            var user = users.SingleOrDefault(x => x.Id == id);
            if (user == null)
                throw new EntityNotFoundException();

            return user;
        }

        public void Update(User user)
        {
            Console.WriteLine("User updated - " + user);
        }
    }
}