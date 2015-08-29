namespace AopDemo.Domain.User
{
    public interface IUserRepository
    {
        User Get(int id);
        void Update(User user);
    }
}
