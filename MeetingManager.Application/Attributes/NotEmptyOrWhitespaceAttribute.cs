using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Attributes
{
    public class NotEmptyOrWhitespaceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string str && string.IsNullOrWhiteSpace(str))
            {
                return new ValidationResult(ErrorMessage ?? "Поле не может быть пустым или состоять только из пробелов");
            }

            return ValidationResult.Success;
        }
    }

}
