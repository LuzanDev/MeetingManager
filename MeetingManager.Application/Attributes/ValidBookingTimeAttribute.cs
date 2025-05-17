using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Attributes
{
    public class ValidBookingTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dto = validationContext.ObjectInstance;

            var startProp = validationContext.ObjectType.GetProperty("StartTime");
            var endProp = validationContext.ObjectType.GetProperty("EndTime");

            if (startProp == null || endProp == null)
                return new ValidationResult("Не удалось найти свойства StartTime или EndTime.");

            var start = startProp.GetValue(dto) as DateTime?;
            var end = endProp.GetValue(dto) as DateTime?;

            if (start == null || end == null)
                return new ValidationResult("Дата начала и окончания обязательны.");

            var now = DateTime.UtcNow;

            if (start < now)
                return new ValidationResult("Дата начала не может быть в прошлом.");

            if (end < now)
                return new ValidationResult("Дата окончания не может быть в прошлом.");

            if (end <= start)
                return new ValidationResult("Дата окончания должна быть позже даты начала.");

            return ValidationResult.Success;
        }
    }
}
