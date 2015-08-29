using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using ValidationResult = Common.Validation.ValidationResult;

namespace Common.Validation
{
    public class DataAnnotationsDtoValidator : IDtoValidator
    {
        public ValidationResult Validate(object dto)
        {
            var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var ctx = new ValidationContext(dto, null, null);
            var isValid = Validator.TryValidateObject(dto, ctx, result, true);
            if (!isValid)
                return CreateValidationResult(result);

            return ValidationResult.ValidResult;
        }

        public void AssertIsValid(object dto)
        {
            if (dto == null)
                return;

            var vr = Validate(dto);
            if (!vr.IsValid)
                throw new DtoValidationException(vr);
        }

        private static ValidationResult CreateValidationResult(IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> result)
        {
            var errors = new List<ValidationError>();
            foreach (var validationResult in result)
            {
                if (validationResult != System.ComponentModel.DataAnnotations.ValidationResult.Success)
                {
                    errors.AddRange(validationResult.MemberNames.Select(x => new ValidationError(validationResult.ErrorMessage, x)));
                }
            }

            return ValidationResult.CreateInvalidResult(errors);
        }
    }
}