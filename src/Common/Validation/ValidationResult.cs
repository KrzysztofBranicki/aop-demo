using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Validation
{
    public class ValidationResult
    {
        public IEnumerable<ValidationError> ValidationErrors { get; private set; }
        public bool IsValid { get { return !ValidationErrors.Any(); } }
        public string Message { get { return IsValid ? null : ValidationErrors.First().Message; } }

        public ValidationResult Merge(ValidationResult other)
        {
            return new ValidationResult(ValidationErrors.Union(other.ValidationErrors));
        }

        protected ValidationResult()
        {
            ValidationErrors = Enumerable.Empty<ValidationError>();
        }

        public ValidationResult(IEnumerable<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public static ValidationResult CreateInvalidResult(IEnumerable<ValidationError> validationErrors)
        {
            return new ValidationResult(validationErrors);
        }

        public static ValidationResult CreateInvalidResult(params ValidationError[] validationErrors)
        {
            return new ValidationResult(validationErrors);
        }

        public static readonly ValidationResult ValidResult = new ValidationResult();
    }

    public class ValidationError
    {
        public string Message { get; private set; }
        public string FieldName { get; private set; }

        public ValidationError(string message, string fieldName = null)
        {
            if (message == null) throw new ArgumentNullException("message");

            Message = message;
            FieldName = fieldName;
        }
    }
}
