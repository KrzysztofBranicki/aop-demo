using System;

namespace AopDemo.Domain.User
{
    public class User
    {
        public int Id { get; private set; }
        public string Password { get; private set; }
        public DateTime? LastPasswordChangeDate { get; private set; }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
            LastPasswordChangeDate = DateTime.Now;
        }

        public User(int id, string password, DateTime? lastPasswordChangeDate)
        {
            Id = id;
            Password = password;
            LastPasswordChangeDate = lastPasswordChangeDate;
        }

        public override string ToString()
        {
            return string.Format("Id: {0} Password: {1} LastPasswordChangeDate: {2}", Id, Password, LastPasswordChangeDate);
        }
    }
}
