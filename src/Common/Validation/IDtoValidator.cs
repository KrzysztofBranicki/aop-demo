namespace Common.Validation
{
    public interface IDtoValidator
    {
        ValidationResult Validate(object dto);
        void AssertIsValid(object dto);
    }
}
