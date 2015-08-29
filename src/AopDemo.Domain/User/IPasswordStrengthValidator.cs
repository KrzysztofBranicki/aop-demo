namespace AopDemo.Domain.User
{
    public interface IPasswordStrengthValidator
    {
        bool IsPasswordGoodEnough(string password);
    }

    public class DummyPasswordStrengthValidator : IPasswordStrengthValidator
    {
        public bool IsPasswordGoodEnough(string password)
        {
            return password.Length > 6;
        }
    }
}
