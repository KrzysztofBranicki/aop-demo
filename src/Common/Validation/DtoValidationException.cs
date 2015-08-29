using System;

namespace Common.Validation
{
    [Serializable]
    public class DtoValidationException : Exception
    {
        public ValidationResult ValidationResult { get; private set; }

        public DtoValidationException(ValidationResult validationResult) : base(validationResult.Message)
        {
            ValidationResult = validationResult;
        }
    }
}